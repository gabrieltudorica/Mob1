using System;
using System.Collections.Generic;

namespace ConsoleApp5
{
    class Program
    {
        static void Main(string[] args)
        {
            Wishlist gabisWishlist = new Wishlist("gabi@gmail.com");

            WishlistItem wish1 = new WishlistItem();
            wish1.Name = "PS5";
            wish1.Link = "https://www.emag.ro/consola-sony-playstation-4-pro-neo-1tb-negru-ps4pro1tb/pd/DVKHF2BBM/?X-Search-Id=9c71edc99dbe26f7b573&X-Product-Id=1221446&X-Search-Page=1&X-Search-Position=0&X-Section=search&X-MB=0&X-Search-Action=view";

            WishlistItem wish2 = new WishlistItem();
            wish2.Name = "1 x Controller PS4 Pro";
            wish2.Link = "https://www.emag.ro/controller-sony-dualshock-4-v2-new-model-pentru-playstation-4-black-ds4v2b/pd/DK5952BBM/?X-Search-Id=2d29283df4bb9a3c3064&X-Product-Id=1269624&X-Search-Page=1&X-Search-Position=0&X-Section=search&X-MB=0&X-Search-Action=view";

            gabisWishlist.AddItem(wish1);
            gabisWishlist.AddItem(wish2);
            gabisWishlist.AddItem(wish2);

            Console.WriteLine("All items");
            DisplayItems(gabisWishlist.ShowAllItems());

            //super good friend care cumpara PS4 :)
            
            Console.WriteLine("Remaining items");
            List<WishlistItem> remainingItems = gabisWishlist.ShowRemainingItems();
                       
            DisplayItems(remainingItems);

            Console.WriteLine("After picking item");
            gabisWishlist.PickItem(remainingItems[0]);
            DisplayItems(gabisWishlist.ShowRemainingItems());

            WishlistItem mouse = new WishlistItem();
            mouse.Name = "Logitech Gaming Mouse";
            mouse.Link = "https://www.emag.ro/mouse-logitech-g403-12000-dpi-pentru-pc-mac-usb-negru-b01kt8d7fm/pd/DDV7KLBBM/?X-Search-Id=ca69a945b1a7716a46cc&X-Product-Id=40535159&X-Search-Page=1&X-Search-Position=0&X-Section=search&X-MB=0&X-Search-Action=view";

            gabisWishlist.PickItem(mouse);
            DisplayItems(gabisWishlist.ShowRemainingItems());

            //owner of wishlist
            List<WishlistItem> allItems = gabisWishlist.ShowAllItems();
            gabisWishlist.RemoveItem(allItems[0]);

            Console.WriteLine("All items");
            DisplayItems(gabisWishlist.ShowAllItems());

            Console.WriteLine("Remaining items");
            DisplayItems(gabisWishlist.ShowRemainingItems());
        }

        private static void DisplayItems(List<WishlistItem> items)
        {
            int index = 1;
            foreach (WishlistItem item in items)
            {
                Console.WriteLine("#" + index + " Name:" + item.Name + " Link: " + item.Link);
                index++;
            }

            Console.WriteLine("------------------------------------------");
        }
    }
}
