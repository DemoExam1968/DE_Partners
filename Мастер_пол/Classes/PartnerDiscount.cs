using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Мастер_пол.Model;

namespace Мастер_пол.Classes
{
    public class PartnerDiscount:Model.Partner
    {
        private int discount;

        //[NotMapped]
        //public string ProductPhotoPath
        //{
        //    get		//Метод возвращает сформированное значение свойство
        //    {
        //        string temp = App.pathExe + @"\Photos\" + this.ProductName + ".jpg";
        //        if (!File.Exists(temp))
        //        {
        //            temp = @"\Resources\picture.png";
        //        }
        //        return temp;
        //    }
        //}





        //        public int Discount { get


        //                int sumProductCount = App.DB.PartnerProduct.Where(pp => pp.PartnerId == partner.PartnerId).Sum(pp => pp.PartnerProductCount);
        //                if (sumProductCount<10000)
        //                {
        //                    partner.PartnerDiscount = 0;
        //                }
        //                else
        //                {
        //                    if (sumProductCount<50000)
        //                    {
        //                        partner.PartnerDiscount = 5;
        //                    }
        //                    else
        //{
        //    if (sumProductCount < 300000)
        //    {
        //        partner.PartnerDiscount = 10;
        //    }
        //    else
        //    {
        //        partner.PartnerDiscount = 15;
        //    }
        //}
        //                }


        //                ; set; }
        public PartnerDiscount() { } 
        
    }
}
