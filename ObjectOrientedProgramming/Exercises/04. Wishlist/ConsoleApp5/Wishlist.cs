using System.Collections.Generic;

namespace ConsoleApp5
{
    public class Wishlist
    {
        public string Owner { get; private set; }
                
        public Wishlist(string owner)
        {
            Owner = owner;            
        }

        private List<WishlistItem> wishlistItems = new List<WishlistItem>();
        public void AddItem(WishlistItem item)
        {
            wishlistItems.Add(item);
        }

        public void RemoveItem(WishlistItem item)
        {
            //this is not needed in this case as an unknown item cannot be removed from
            //our list => this is also called defensive programming and is usually not ok (exceptions apply)
            if (wishlistItems.Contains(item))
            {
                wishlistItems.Remove(item);
            }
        }

        public void PickItem(WishlistItem item)
        {
            if (wishlistItems.Contains(item))
            {
                item.IsPicked = true;
            }
        }

        public List<WishlistItem> ShowAllItems()
        {
            return wishlistItems;
        }

        public List<WishlistItem> ShowRemainingItems()
        {
            return wishlistItems.FindAll(x => !x.IsPicked);

            //List<WishlistItem> remainingItems = new List<WishlistItem>();

            //foreach (WishlistItem item in wishlistItems)
            //{
            //    if (!item.IsPicked)
            //    {
            //        remainingItems.Add(item);
            //    }
            //}

            //return remainingItems;
        }

        private List<string> friendEmails = new List<string>();

        public void ShareWith(List<string> friendEmails)
        {
            this.friendEmails = friendEmails;
        }

        public bool IsSharedWith(string email)
        {
            return friendEmails.Contains(email);
        }
    }
}
