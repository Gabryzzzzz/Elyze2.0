// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Elyze.Helpers.Headers
{

    public class HeaderAction : BreadClass
    {

    }

    public class BreadClass
    {
        public string Name { get; set; } = "";
        public string? Link { get; set; }

        public static List<BreadClass> CreateBread(string[] list)
        {

            List<BreadClass> bread = new();

            for (var i = 0; i < list.Length - 1; i += 2)
            {
                bread.Add(new BreadClass
                {
                    Name = list[i],
                    Link = list[i + 1]
                });
            }

            bread.Add(new BreadClass
            {
                Name = list[^1],
                Link = ""
            });

            return bread;
        }

    }

    public static partial class HeaderClassBuilders
    {

    }

    public class HeaderClass
    {
        public string Title { get; set; } = "Default";
        public string BackRoute { get; set; } = "";
        public List<BreadClass> Breadcrumb { get; set; } = new();
        public HeaderAction HeaderAction { get; set; } = new();
    }
}
