#pragma checksum "E:\CakeShop\MainProject\TAI_RLaba_PNakielny\Views\Home\MainPage.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5049f8a2096dc0ebb9bf13378c96bbd9fb37d3e5"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_MainPage), @"mvc.1.0.view", @"/Views/Home/MainPage.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "E:\CakeShop\MainProject\TAI_RLaba_PNakielny\Views\_ViewImports.cshtml"
using CakeShop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "E:\CakeShop\MainProject\TAI_RLaba_PNakielny\Views\_ViewImports.cshtml"
using CakeShop.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "E:\CakeShop\MainProject\TAI_RLaba_PNakielny\Views\Home\MainPage.cshtml"
using System.Security.Claims;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5049f8a2096dc0ebb9bf13378c96bbd9fb37d3e5", @"/Views/Home/MainPage.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9a25558179c3851a9f8353673bf3796eb9fce592", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_MainPage : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "E:\CakeShop\MainProject\TAI_RLaba_PNakielny\Views\Home\MainPage.cshtml"
  
    ViewData["Title"] = "Main page";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n");
            WriteLiteral("\r\n\r\n    <div class=\"text-center\">\r\n        <h1 class=\"display-4\">\r\n            Cukiernia KarmeLove\r\n");
            WriteLiteral(@"</h1>
</div>

<div class=""card-group"">
  <div class=""card"">
    <img href=""Cake"" class=""card-img-top"" src=""images/offer.jpg"" alt=""Card image cap"">
    <div class=""card-body"">
      <h5 class=""card-title"" href=""Cake"">Lista ciast</h5>
      <p class=""card-text"">Lista dostępnych ciast.</p>
      <a href=""Cake"" class=""card-link"">Do ciast</a>
    </div>
  </div>
  <div class=""card"">
    <img class=""card-img-top"" src=""images/price_list.jpg"" alt=""Card image cap"">
    <div class=""card-body"">
      <h5 class=""card-title"">Koszyk</h5>
      <p class=""card-text"">Twoje dodane ciasta.</p>
      <a href=""Cart"" class=""card-link"">Do koszyka</a>
    </div>
  </div>
  <div class=""card"">
    <img class=""card-img-top"" src=""images/contact.jpg"" alt=""Card image cap"">
    <div class=""card-body"">
      <h5 class=""card-title"">Polityka prywatności</h5>
      <a href=""Home/Privacy"" class=""card-link"">Do polityki</a>
    </div>
  </div>
 </div>
");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
