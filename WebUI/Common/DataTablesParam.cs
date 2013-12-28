using System.Collections.Generic;

namespace WebUI.Common
{
    public class DataTablesParam
    {
        public bool bEscapeRegex { get; set; }
        public List<bool> bEscapeRegexColumns { get; set; }
        public List<bool> bSearchable { get; set; }
        public List<bool> bSortable { get; set; }
        public int iColumns { get; set; }
        public int iDisplayLength { get; set; }
        public int iDisplayStart { get; set; }
        public List<int> iSortCol { get; set; }
        public int iSortingCols { get; set; }
        public int sEcho { get; set; }
        public string sSearch { get; set; }
        public List<string> sSearchColumns { get; set; }
        public List<string> sSortDir { get; set; }
    }
}