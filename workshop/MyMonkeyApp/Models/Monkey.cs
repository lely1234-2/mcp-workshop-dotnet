namespace MyMonkeyApp.Models
{
    public class Monkey
    {
        public string Name { get; set; }
        public string Species { get; set; }
        public int Age { get; set; }
        public string FavoriteFood { get; set; }
        public string Description { get; set; }
        // 필요에 따라 추가 속성 가능 (예: public string AsciiArt { get; set; })
    }
}
