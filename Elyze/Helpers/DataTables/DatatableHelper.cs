// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Elyze.Helpers.DataTables
{



    // new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data }
    public class DataTableResult
    {
        public int Draw { get; set; }
        public int RecordsTotal { get; set; }
        public int RecordsFiltered { get; set; }
        public List<object> Data { get; set; } = new();
    }

    public class DataTableForm
    {
        public int Draw { get; set; } = 0;
        public int Start { get; set; } = 0;
        public int Length { get; set; } = 10;
        public string SortColumn { get; set; } = "";
        public string SortColumnDirection { get; set; } = "";
        public string SearchValue { get; set; } = "";
        public string OrderBy { get; set; } = "";
    }

    public class HelpersDataTables
    {
        public static DataTableForm ContextToDatatable(HttpRequest request)
        {

            DataTableForm dataTableParameters = new()
            {
                Draw = Convert.ToInt32(request.Form["draw"]),
                Start = Convert.ToInt32(request.Form["start"]),
                Length = Convert.ToInt32(request.Form["length"]),
                SortColumn = request.Form["columns[" + request.Form["order[0][column]"] + "][name]"],
                SortColumnDirection = request.Form["order[0][dir]"],
                SearchValue = request.Form["search[value]"]
            };

            dataTableParameters.OrderBy = dataTableParameters.SortColumn + " " + dataTableParameters.SortColumnDirection;
            return dataTableParameters;
        }

    }


}
