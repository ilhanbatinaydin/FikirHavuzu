using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using FikirHavuzu.Web.Models;

namespace FikirHavuzu.Web.Infrastructure.TagHelpers
{
    [HtmlTargetElement("div", Attributes = "page-model")]
    public class PageLinkTagHelper : TagHelper
    {
        private readonly IUrlHelperFactory _urlHelperFactory;

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext? ViewContext { get; set; }

        public Pagination PageModel { get; set; }

        public String? PageAction { get; set; }

        public bool PageClassesEnabled { get; set; } = false;

        public string PageClass { get; set; } = String.Empty;

        public string PageClassNormal { get; set; } = String.Empty;

        public string PageClassSelected { get; set; } = String.Empty;

        public PageLinkTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            _urlHelperFactory = urlHelperFactory;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (ViewContext is not null && PageModel is not null)
            {
                IUrlHelper urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);
                TagBuilder result = new TagBuilder("div");

                // Kaç sayfa butonu gösterileceğini belirleyelim (örn: aktif sayfanın 2 öncesi, 2 sonrası)
                int visiblePages = 5;
                int startPage = Math.Max(1, PageModel.CurrentPage - (visiblePages / 2));
                int endPage = Math.Min(PageModel.TotalPages, startPage + visiblePages - 1);

                if (endPage - startPage + 1 < visiblePages)
                {
                    startPage = Math.Max(1, endPage - visiblePages + 1);
                }

                // 1. "Önceki" Butonu (Sadece 1. sayfada değilsek göster)
                if (PageModel.CurrentPage > 1)
                {
                    result.InnerHtml.AppendHtml(CreatePageTag(urlHelper, PageModel.CurrentPage - 1, "Önceki", false));
                }

                // 2. İlk Sayfa ve Üç Nokta (...) (Eğer başlangıç 1'den büyükse)
                if (startPage > 1)
                {
                    result.InnerHtml.AppendHtml(CreatePageTag(urlHelper, 1, "1", false));
                    if (startPage > 2)
                    {
                        result.InnerHtml.AppendHtml(CreateEllipsisTag());
                    }
                }

                // 3. Orta Sayfalar (Hesapladığımız pencere)
                for (int i = startPage; i <= endPage; i++)
                {
                    bool isSelected = i == PageModel.CurrentPage;
                    result.InnerHtml.AppendHtml(CreatePageTag(urlHelper, i, i.ToString(), isSelected));
                }

                // 4. Son Sayfa ve Üç Nokta (...) (Eğer bitiş son sayfadan küçükse)
                if (endPage < PageModel.TotalPages)
                {
                    if (endPage < PageModel.TotalPages - 1)
                    {
                        result.InnerHtml.AppendHtml(CreateEllipsisTag());
                    }
                    result.InnerHtml.AppendHtml(CreatePageTag(urlHelper, PageModel.TotalPages, PageModel.TotalPages.ToString(), false));
                }

                // 5. "Sonraki" Butonu (Sadece son sayfada değilsek göster)
                if (PageModel.CurrentPage < PageModel.TotalPages)
                {
                    result.InnerHtml.AppendHtml(CreatePageTag(urlHelper, PageModel.CurrentPage + 1, "Sonraki", false));
                }

                output.Content.AppendHtml(result.InnerHtml);
            }
        }

        private TagBuilder CreatePageTag(IUrlHelper urlHelper, int pageNumber, string text, bool isSelected)
        {
            TagBuilder tag = new TagBuilder("a");

            var routeValues = new RouteValueDictionary();

            foreach (var key in ViewContext.RouteData.Values.Keys)
            {
                routeValues[key] = ViewContext.RouteData.Values[key];
            }

            foreach (var key in ViewContext.HttpContext.Request.Query.Keys)
            {
                routeValues[key] = ViewContext.HttpContext.Request.Query[key].ToString();
            }
            routeValues["PageNumber"] = pageNumber;

            tag.Attributes["href"] = urlHelper.Action(PageAction, routeValues);

            if (PageClassesEnabled)
            {
                tag.AddCssClass(PageClass);
                tag.AddCssClass(isSelected ? PageClassSelected : PageClassNormal);
            }

            tag.InnerHtml.Append(text);
            return tag;
        }

        private TagBuilder CreateEllipsisTag()
        {
            TagBuilder tag = new TagBuilder("span");
            tag.AddCssClass(PageClass);
            tag.AddCssClass("disabled text-muted");
            tag.InnerHtml.Append("...");
            return tag;
        }
    }
}