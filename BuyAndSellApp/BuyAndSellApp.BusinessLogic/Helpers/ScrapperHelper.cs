using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuyAndSellApp.BusinessLogic.DataStructures;
using BuyAndSellApp.BusinessLogic.Scrappers;

namespace BuyAndSellApp.BusinessLogic.Helpers
{
    public class ScrapperHelper
    {
        public async Task<IEnumerable<ProductResponse>> GetProductList(IEnumerable<string> keywords)
        {
            try
            {
                if (!keywords.Any())
                    return null;

                var threadLock = new object();
                var list = new List<ProductResponse>();


                var taskList = new List<Task<ProductResponse>>();
                var carousellScrapper = new CarousellScrapper();
                var olxScrapper = new OLXScrapper();

                foreach (var keyword in keywords)
                {
                    var carousellRequest = new ProductRequest {Keyword = keyword};
                    taskList.Add(Task.Run(() =>
                    {
                        var result = carousellScrapper.GetProductList(carousellRequest);
                        lock (threadLock)
                        {
                            list.Add(result);
                        }
                        return result;
                    }));

                    var olxRequest = new ProductRequest {Keyword = keyword, Page = 1};
                    taskList.Add(Task.Run(() =>
                    {
                        var result = olxScrapper.GetProductList(olxRequest);
                        lock (threadLock)
                        {
                            list.Add(result);
                        }
                        return result;
                    }));
                }

                Task.WaitAll(taskList.ToArray());
                return list;
            }
            catch (AggregateException arrException)
            {
                throw arrException;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
