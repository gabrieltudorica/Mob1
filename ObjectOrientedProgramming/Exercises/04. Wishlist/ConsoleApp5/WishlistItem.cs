namespace ConsoleApp5
{
    public class WishlistItem
    {
        public string Name { get; set; }
        public string Link { get; set; }
        public bool IsPicked {get; set;}

        public WishlistItem()
        {
            IsPicked = false;
        }
    }
}
