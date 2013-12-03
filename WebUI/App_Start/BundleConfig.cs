using System.Web.Optimization;

namespace WebUI.App_Start
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-ui-min").Include(
                        "~/Scripts/jquery-ui-{version}.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
                        "~/Scripts/DataTables-1.9.4/media/js/*.js"));

            bundles.Add(new ScriptBundle("~/bundles/receipts_index").Include(
                         "~/Scripts/jquery-ui-{version}.js",
                        "~/Scripts/DataTables-1.9.4/media/js/*.js", //todo need to be specific rather then *
                         "~/Scripts/LMate/Receipts_Index.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/receipts_edit").Include(
                "~/Scripts/jquery-ui-{version}.js",
                "~/Scripts/select2.js",
                "~/Scripts/LMate/Receipts_Edit.js"
            ));

            //========== style ===========//
            bundles.Add(new StyleBundle("~/Content/jquery-ui").Include(
                                      "~/Content/themes/base/jquery-ui.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                                            "~/Content/bootstrap.css",
                                            "~/Content/landlord.css"));

            bundles.Add(new StyleBundle("~/Content/frontpage").Include(
                                            "~/Content/bootstrap.css",
                                            "~/Content/site.css",
                                            "~/Content/mobile.css",
                                            "~/Content/login.css"));

            bundles.Add(new ScriptBundle("~/Content/datatables/css").Include(
                        "~/Content/DataTables-1.9.4/media/css/*.css"));

            bundles.Add(new ScriptBundle("~/Content/receipts_index").Include(
                      "~/Content/DataTables-1.9.4/media/css/*.css",//todo need to be specific rather then *
                      "~/Content/themes/base/jquery-ui.css"));

            bundles.Add(new ScriptBundle("~/Content/receipts_edit").Include(
                  "~/Content/css/select2.css",
                  "~/Content/themes/base/jquery-ui.css"));
        }
    }
}
