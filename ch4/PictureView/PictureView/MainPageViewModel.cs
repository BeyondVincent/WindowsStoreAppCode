using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureView
{
    class MainPageViewModel
    {
        public List<ImageData> itemsList { get; set; }

        public MainPageViewModel()
        {
            itemsList = new List<ImageData>();
            itemsList.Add(new ImageData("抚仙湖", "image/1.jpg", "抚仙湖\n环境非常宜人"));
            itemsList.Add(new ImageData("大理洱海", "image/2.jpg", "大理洱海\n冬季的洱海"));
            itemsList.Add(new ImageData("澜沧江畔", "image/3.jpg", "澜沧江畔\n清澈的河水"));
            itemsList.Add(new ImageData("北京马拉松", "image/4.jpg", "北京马拉松\n天安门长安街起跑"));
            itemsList.Add(new ImageData("家门口", "image/5.jpg", "家门口\n春天的油菜花"));
            itemsList.Add(new ImageData("玉龙雪山", "image/6.jpg", "玉龙雪山\n美丽的高原雪山"));

            itemsList.Add(new ImageData("抚仙湖", "image/1.jpg", "抚仙湖\n环境非常宜人"));
            itemsList.Add(new ImageData("大理洱海", "image/2.jpg", "大理洱海\n冬季的洱海"));
            itemsList.Add(new ImageData("澜沧江畔", "image/3.jpg", "澜沧江畔\n清澈的河水"));
            itemsList.Add(new ImageData("北京马拉松", "image/4.jpg", "北京马拉松\n天安门长安街起跑"));
            itemsList.Add(new ImageData("家门口", "image/5.jpg", "家门口\n春天的油菜花"));
            itemsList.Add(new ImageData("玉龙雪山", "image/6.jpg", "玉龙雪山\n美丽的高原雪山"));

            itemsList.Add(new ImageData("抚仙湖", "image/1.jpg", "抚仙湖\n环境非常宜人"));
            itemsList.Add(new ImageData("大理洱海", "image/2.jpg", "大理洱海\n冬季的洱海"));
            itemsList.Add(new ImageData("澜沧江畔", "image/3.jpg", "澜沧江畔\n清澈的河水"));
            itemsList.Add(new ImageData("北京马拉松", "image/4.jpg", "北京马拉松\n天安门长安街起跑"));
            itemsList.Add(new ImageData("家门口", "image/5.jpg", "家门口\n春天的油菜花"));
            itemsList.Add(new ImageData("玉龙雪山", "image/6.jpg", "玉龙雪山\n美丽的高原雪山"));
        }
    }
}
