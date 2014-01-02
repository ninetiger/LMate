using System.Web.Optimization;

namespace WebUI.App_Start
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            //todo need to tidy up to seperate minified versions
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


            //BasicPlus js
            bundles.Add(new ScriptBundle("~/bundles/fileupload").Include(
                 "~/Scripts/FileUpload/jqueryui/jquery.ui.widget.js",
                 "~/Scripts/FileUpload/load-image.debug.js",
                 "~/Scripts/FileUpload/canvas-to-blob.debug.js",
                 "~/Scripts/FileUpload/jquery.iframe-transport.js",
                 "~/Scripts/FileUpload/jquery.fileupload.js",
                 "~/Scripts/FileUpload/jquery.fileupload-process.js",
                 "~/Scripts/FileUpload/jquery.fileupload-image.js",
                 "~/Scripts/FileUpload/jquery.fileupload-audio.js",
                 "~/Scripts/FileUpload/jquery.fileupload-video.js",
                 "~/Scripts/FileUpload/jquery.fileupload-validate.js",
                 "~/Scripts/LMate/FileUpload.js"
            ));
            //BasicPlus css
            bundles.Add(new StyleBundle("~/Content/fileupload").Include(
                //"~/Content/FileUpload/css/bootstrap/bootstrap.debug.css",
                //"~/Content/FileUpload/css/bootstrap/bootstrap-responsive.debug.css",
               "~/Scripts/bootstrap.js",
               "~/Scripts/respond.js",
               "~/Content/FileUpload/css/jquery.fileupload-ui.css",
               "~/Content/LMate/FileUploadCustom.css"
           ));

            //========== style ===========//
            bundles.Add(new StyleBundle("~/Content/jquery-ui").Include(
                                      "~/Content/themes/base/jquery-ui.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                                            "~/Content/bootstrap.css",
                                            "~/Content/landlord.css",
                                            "~/Content/share.css"
                                            ));



            bundles.Add(new ScriptBundle("~/Content/datatables/css").Include(
                        "~/Content/DataTables-1.9.4/media/css/*.css"));


            #region frontpage
            bundles.Add(new StyleBundle("~/Content/frontpage").Include(
                                "~/Content/bootstrap.css",
                                "~/Content/site.css",
                                "~/Content/shared.css",
                                "~/Content/mobile.css",
                                "~/Content/login.css"));

            bundles.Add(new ScriptBundle("~/bundles/frontpage").Include(
                      "~/Scripts/LMate/Frontpage.js"
            ));
            #endregion

            #region receipt_index
            bundles.Add(new ScriptBundle("~/bundles/receipts_index").Include(
                "~/Scripts/jquery-ui-{version}.js",
                "~/Scripts/DataTables-1.9.4/media/js/jquery.dataTables.js", //todo need to be specific rather then *
                //"~/Scripts/jquery.dataTables.columnFilter.js",
               "~/Scripts/LMate/Receipts_Index.js"
            ));

            bundles.Add(new ScriptBundle("~/Content/receipts_index").Include(
                "~/Content/DataTables-1.9.4/media/css/jquery.dataTables.css",//todo need to be specific rather then *
                "~/Content/themes/base/jquery-ui.css"));
            #endregion

            #region receipt_edit
            bundles.Add(new ScriptBundle("~/Content/receipts_edit").Include(
              "~/Content/css/select2.css"
              , "~/Content/themes/base/jquery-ui.css"
              , "~/Content/LMate/Receipts_Edit.css"
              , "~/Scripts/iViewer/jquery.iviewer.css"
              ));

            bundles.Add(new ScriptBundle("~/bundles/receipts_edit").Include(
                "~/Scripts/jquery-ui-{version}.js",
                "~/Scripts/select2.js",
                "~/Scripts/LMate/Receipts_Edit.js",
                "~/Scripts/iViewer/jquery.iviewer.js"
            ));
            #endregion


        }
    }
}
