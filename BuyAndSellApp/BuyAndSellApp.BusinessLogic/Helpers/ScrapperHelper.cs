using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuyAndSellApp.BusinessLogic.DataStructures;
using BuyAndSellApp.BusinessLogic.Scrappers;
using BuyAndSellApp.Entities.Enums;

namespace BuyAndSellApp.BusinessLogic.Helpers
{
    public class ScrapperHelper
    {
        public async Task<IEnumerable<ProductResponse>> GetProductList(IEnumerable<ProductRequest> requests)
        {
            try
            {
                if (!requests.Any())
                    return null;

                var threadLock = new object();
                var list = new List<ProductResponse>();


                var taskList = new List<Task<ProductResponse>>();
                var carousellScrapper = new CarousellScrapper();
                var olxScrapper = new OLXScrapper();

                foreach (var request in requests)
                {
                    if (request.Source == ProductSource.Carousell)
                    {
                        taskList.Add(Task.Run(() =>
                        {
                            var result = String.IsNullOrEmpty(request.NextPage)
                                ? carousellScrapper.GetProductList(request)
                                : carousellScrapper.GetProductList(request.NextPage);
                            lock (threadLock)
                            {
                                list.Add(result);
                            }
                            return result;
                        }));
                    }

                    if (request.Source == ProductSource.Olx)
                    {
                        var olxRequest = new ProductRequest { Keyword = request.Keyword, Page = request.Page};
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
