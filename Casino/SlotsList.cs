using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino
{
    public class SlotsList
    {
        public static List<SlotPreview> GetAllSlots()
        {
            return new List<SlotPreview>()
            {
                new SlotPreview("Hat Slot", "<h3 style='color: white;'>Шляпа, слот, крутіть, зарабативать, дєньгі, анлайн, бєз вірусав</h3>"),
                new SlotPreview("Construct the Mobster", "<h3 style='color: white;'>Бєдний мафєози патєрял ногу в бучє:(</h3>\n<h4 style='color: white;'>Но єго сутьба в надєжних, удачних руках!</h4>\n<h4 style='color: white;'>Помогитє бєдному мафіозє собрать свойо тєло.</h4>"),
                new SlotPreview("Shoot the Beer", "<h3 style='color: white;'>Папробуй пападі па піву!</h3>\n<h4 style='color: white;'>Нє каждий так сможет..! Но я вєрю, у вас фсьо палучітца!</h4>"),
                //new SlotPreview("Hat Slot"),
                //new SlotPreview("Hat Slot"),
                //new SlotPreview("Hat Slot"),
                //new SlotPreview("Hat Slot"),
                //new SlotPreview("Hat Slot"),
                //new SlotPreview("Hat Slot"),
                //new SlotPreview("Hat Slot"),
                //new SlotPreview("Hat Slot"),
                //new SlotPreview("Hat Slot"),
                //new SlotPreview("Hat Slot"),
                //new SlotPreview("Hat Slot"),
                //new SlotPreview("Hat Slot"),
                //new SlotPreview("Hat Slot")
            };
        }
    }
    public class SlotPreview
    {
        public string Name = string.Empty;
        public string Image = string.Empty;
        public string Action = string.Empty;
        public string PrevImage = string.Empty;
        public string Description = string.Empty;

        public SlotPreview() { }
        public SlotPreview(string name, string description = "")
        {
            Name = name;
            Image = "/SlotImages/" + name.Replace(" ", "") + ".png";
            Action = name.ToLower().Replace(" ", "");
            Description = description;
            PrevImage = "/SlotImages/" + name.Replace(" ", "") + "Prev.png";
        }
    }
}
