using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using SportsStore.WebUI.HtmlHelpers;
using SportsStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.UnitTests
{
    public class PageLinksTests
    {
        [Fact]
        public void CanGeneratePageLinks()
        {
            // Arrange
            HtmlHelper myHelper = null;
            var pagingInfo = new PagingInfo()
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };
            var pageUrlDelegate = (int i) => "Page" + i;

            var actual1 = new TagBuilder("a");
            actual1.MergeAttribute("href", "Page1");
            actual1.InnerHtml.AppendHtml("1");
            actual1.AddCssClass("btn btn-default");

            var actual2 = new TagBuilder("a");
            actual2.MergeAttribute("href", "Page1");
            actual2.InnerHtml.AppendHtml("2");
            actual2.AddCssClass("btn-primary");
            actual2.AddCssClass("btn btn-default");

            // Act
            var result = myHelper.PageLinks(pagingInfo, pageUrlDelegate).ToArray();

            //Assert
            Assert.Equal(actual1.ToString(), result[0].ToString());
            Assert.Equal(actual2.ToString(), result[1].ToString());
        }
    }
}
