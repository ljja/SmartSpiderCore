using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using HtmlAgilityPack;
using SmartSpiderCore.ExtractField;
using SmartSpiderCore.ExtractRule;
using SmartSpiderCore.In;
using SmartSpiderCore.Out;

namespace SmartSpiderCore
{
    [
    XmlInclude(typeof(LocalDirectoryInput)), XmlInclude(typeof(HttpInput)), XmlInclude(typeof(FileInput)), XmlInclude(typeof(FileLineEnumeratorRule)),
    XmlInclude(typeof(ActiveMQInput)), XmlInclude(typeof(ActiveMQEnumerator)),

    XmlInclude(typeof(DateRule)), XmlInclude(typeof(ReplaceRule)), XmlInclude(typeof(RegexRule)),
    XmlInclude(typeof(SubStringRule)), XmlInclude(typeof(TrimRule)), XmlInclude(typeof(XPathRule)),
    XmlInclude(typeof(SubStringIndexRule)), XmlInclude(typeof(SessionRule)), XmlInclude(typeof(Base64Rule)),
    XmlInclude(typeof(GuidRule)), XmlInclude(typeof(SplitRule)), XmlInclude(typeof(RegexToList)),
    XmlInclude(typeof(FixedValueRule)), XmlInclude(typeof(InnerTextRule)),XmlInclude(typeof(Md5Rule)),

    XmlInclude(typeof(ComplexField)),
    XmlInclude(typeof(RegexNavigationRule)),

    XmlInclude(typeof(SqlServerOutput)), XmlInclude(typeof(CsvOutput)), XmlInclude(typeof(SOHOutput)),
    ]
    public class SpiderEngine : IDisposable
    {
        #region 公共属性

        public string Title { get; set; }

        public string Description { get; set; }

        public List<Input> InputList { get; set; }

        public NavigationRule NavigationRule { get; set; }

        public List<Field> FieldList { get; set; }

        public List<Output> OutputList { get; set; }

        public string TableXPath { get; set; }

        public bool IsSplitTable { get; set; }

        #endregion

        #region 公共方法

        public SpiderEngine()
        {
            Title = string.Empty;
            Description = string.Empty;
            InputList = new List<Input>();
            OutputList = new List<Output>();
            FieldList = new List<Field>();
            IsSplitTable = false;
            TableXPath = string.Empty;
        }

        public void Init()
        {
            foreach (var m in InputList)
            {
                m.Init();
            }

            foreach (var m in OutputList)
            {
                m.Init();
            }
        }

        public void Exec()
        {
            foreach (var inputItem in InputList)
            {
                var inputEnumerator = inputItem.GetEnumerator();
                while (inputEnumerator.MoveNext())
                {
                    //起始地址
                    var content = inputItem.GetContent(inputEnumerator.Current);

                    //导航地址
                    if (NavigationRule != null)
                    {
                        var urlCollection = NavigationRule.Exec(content);
                        if (urlCollection.Count > 0)
                        {
                            foreach (var subUrl in urlCollection)
                            {
                                content = inputItem.GetContent(subUrl);
                                //解析终端页
                                ParseContent(content);
                            }
                        }
                    }
                    else
                    {
                        //解析终端页
                        ParseContent(content);
                    }
                }

                //重置输入
                inputEnumerator.Reset();
            }
        }

        public void Dispose()
        {
            InputList.Clear();
            FieldList.Clear();

            foreach (var m in OutputList)
            {
                m.Dispose();
            }
            OutputList.Clear();
        }

        #endregion

        #region 私有方法

        private void ParseContent(Content content)
        {
            if (IsSplitTable)
            {
                ExtractTableFieldResult(content);
            }
            else
            {
                ExtractFieldResult(content);
            }
        }

        /// <summary>
        /// Install-Package Jumony.Core
        /// //*[@id="jingzhun"]
        /// //*[@id="infolist"]/table
        /// </summary>
        /// <param name="content"></param>
        private void ExtractTableFieldResult(Content content)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(content.ContentText);

            var tableNode = doc.DocumentNode.SelectSingleNode(TableXPath);

            if (tableNode == null) return;

            var trNodes = tableNode.Elements("tr");

            foreach (var tr in trNodes)
            {
                var fieldResultList = new List<FieldResult>();
                var tdNodes = tr.Elements("td").ToList();

                for (var i = 0; i < tdNodes.Count; i++)
                {
                    var fields = FieldList.Where(p => p.Index == i).ToList();
                    foreach (var m in fields)
                    {
                        var con = new Content
                        {
                            ContentText = tdNodes[i].InnerHtml,
                            Session = content.Session
                        };
                        var item = new FieldResult
                        {
                            Title = m.Title,
                            DataName = m.DataName,
                            DataValue = m.Exec(con).ContentText,
                            Require = m.Require,
                            Sort = m.Sort
                        };
                        fieldResultList.Add(item);
                    }
                }

                PublishResult(fieldResultList);
            }
        }

        private void ExtractFieldResult(Content content)
        {
            var fieldResultList = new List<FieldResult>();
            foreach (var m in FieldList)
            {
                var objContent = content.Clone();

                //主要规则
                var item = new FieldResult
                {
                    Title = m.Title,
                    DataName = m.DataName,
                    DataValue = m.Exec(objContent as Content).ContentText,
                    Require = m.Require,
                    Sort = m.Sort
                };

                //备用规则
                if (m.Require && string.IsNullOrEmpty(item.DataValue))
                {
                    item.DataValue = m.Exec2(objContent as Content).ContentText;
                }

                fieldResultList.Add(item);
            }

            PublishResult(fieldResultList);
        }

        private void PublishResult(List<FieldResult> fieldResultList)
        {
            fieldResultList = fieldResultList.OrderBy(p => p.Sort).ThenBy(p => p.Title).ToList();

            var validationResult = true;
            foreach (var m in fieldResultList)
            {
                if (m.Validation == false)
                {
                    validationResult = false;
                    break;
                }
            }

            if (validationResult)
            {
                foreach (var outputItem in OutputList)
                {
                    try
                    {
                        outputItem.Write(fieldResultList);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }
            else
            {
                Console.WriteLine("采集结果验证失败,内容已忽略");
            }
        }

        #endregion
    }
}