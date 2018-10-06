using System;

namespace ServeAdmin
{
    public class Item
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }

        public Item(int id, string text, string description) {
            Id = id;
            Text = text;
            Description = description;
        } 
        public Item() {}
        public Item SetText(String text) {
            return new Item(Id, text, Description);
        }
    }
}
