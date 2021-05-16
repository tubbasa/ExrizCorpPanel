using ExrizCorpPanel.Data.Models.DB;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExrizCorpPanel.Core.Infrastructure
{
    public interface ISliderRepository:IRepository<Slider>
    {

        int InsertAndGetId(Slider slider);
    }
}
