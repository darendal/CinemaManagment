﻿using System.Web;
using System.Web.Optimization;

namespace CinemaManagerWeb
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include("~/Scripts/angular.*"));
            bundles.Add(new ScriptBundle("~/bundles/angular-animate").Include(
                "~/Scripts/angular-animate/angular-animate.*"));
            bundles.Add(new ScriptBundle("~/bundles/angular-aria").Include("~/Scripts/angular-aria/angular-aria.*"));
            bundles.Add(new ScriptBundle("~/bundles/angular-material").Include(
                "~/Scripts/angular-material/angular-material.*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      //"~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/angular-material").Include(
                "~/Content/angular-material.css", "~/Content/angular-material.min.css"));

        }
    }
}