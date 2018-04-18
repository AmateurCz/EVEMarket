using System;
using System.Collections.Generic;
using System.Text;
using YamlDotNet.Serialization;

namespace EVEMarket.Model
{
    public class Name
    {
        [YamlMember(Alias = "itemID")]
        public int Id { get; set; }

        [YamlMember(Alias = "itemName")]
        public string ItemName { get; set; }
    }
}
