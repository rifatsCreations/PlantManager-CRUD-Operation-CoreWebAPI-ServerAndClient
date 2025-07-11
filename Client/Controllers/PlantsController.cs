using Client.Models;
using Client.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Client.Controllers
{
    public class PlantsController : Controller
    {
        private string apiUrl = "http://localhost:5071/api/Plants";
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewBag.ApiUrl = "http://localhost:5071";
            List<Plant> plantList = new List<Plant>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(apiUrl))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    plantList = JsonConvert.DeserializeObject<List<Plant>>(apiResponse);
                }
            }
            return View(plantList);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            Plant plant = new Plant();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{apiUrl}/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    plant = JsonConvert.DeserializeObject<Plant>(apiResponse);
                }
            }
            return View(plant);
        }
        public async Task<IActionResult> Delete(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync(apiUrl + $"?id={id}"))
                {
                    if (response.IsSuccessStatusCode)
                        return RedirectToAction("Index");
                }


            }
            return RedirectToAction("Index");

        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            PlantViewModel Pvm = new PlantViewModel();
            return View(Pvm);
        }
        [HttpPost]

        public async Task<IActionResult> Create(PlantViewModel plant)

        {
            Common obj = new Common();
            obj.PlantCareTypes = JsonConvert.SerializeObject(plant.PlantCareTypes, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            using var content = new MultipartFormDataContent();
            content.Add(new StringContent(plant.PlantId.ToString()), "PlantId");
            content.Add(new StringContent(plant.PlantName.ToString()), "PlantName");
            content.Add(new StringContent(plant.Date.ToString("yyyy-MM-dd")), "Date");
            content.Add(new StringContent(plant.IsFlowerBearing.ToString()), "IsFlowerBearing");
            content.Add(new StringContent(plant.PlantPrice.ToString()), "PlantPrice");

            if (plant.ProfileFile != null)
            {
                content.Add(new StreamContent(plant.ProfileFile.OpenReadStream()), "ImageFile", plant.ProfileFile.FileName);
                content.Add(new StringContent(plant.ProfileFile.FileName), "ImageName");
            }
            content.Add(new StringContent(obj.PlantCareTypes), "PlantCareTypes");
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsync(apiUrl, content);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return StatusCode((int)response.StatusCode, "Error occurred while creating plant.");
                    }
                }
            }
            catch (Exception ex)
            {

                return StatusCode((500), ex.Message);
            }

        }

        
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.ApiUrl = "http://localhost:5071";
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(apiUrl + $"/{id}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        var plant = JsonConvert.DeserializeObject<Plant>(apiResponse);
                        if (plant != null)
                        {
                            var plantViewModel = new PlantViewModel
                            {
                                PlantId = plant.PlantId,
                                PlantName = plant.PlantName,
                                IsFlowerBearing = plant.IsFlowerBearing,
                                Date = plant.Date,
                                PlantPrice=plant.PlantPrice,
                                ImageUrl = plant.ImageUrl,
                                PlantCareTypes = plant.PlantCareTypes.ToList()
                            };
                            return View(plantViewModel);
                        }
                        else
                        {
                            return StatusCode((int)response.StatusCode, "Plant not found.");
                        }
                    }
                }
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(PlantViewModel plant)
        {
            Common obj = new Common();
            obj.PlantCareTypes = JsonConvert.SerializeObject(plant.PlantCareTypes, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            using var content = new MultipartFormDataContent();
            content.Add(new StringContent(plant.PlantId.ToString()), "PlantId");
            content.Add(new StringContent(plant.PlantName.ToString()), "PlantName");
            content.Add(new StringContent(plant.Date.ToString("yyyy-MM-dd")), "Date");
            content.Add(new StringContent(plant.IsFlowerBearing.ToString()), "IsFlowerBearing");
            content.Add(new StringContent(plant.PlantPrice.ToString()), "PlantPrice");
            if (plant.ProfileFile != null)
            {
                content.Add(new StreamContent(plant.ProfileFile.OpenReadStream()), "ImageFile", plant.ProfileFile.FileName);
                content.Add(new StringContent(plant.ProfileFile.FileName), "ImageName");
            }
            else
            {
                if (!string.IsNullOrEmpty(plant.ImageUrl))
                {
                    content.Add(new StringContent(plant.ImageUrl), "ImageName");
                }
            }
            content.Add(new StringContent(obj.PlantCareTypes), "PlantCareTypes");
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PutAsync(apiUrl + $"?id={plant.PlantId}", content);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return StatusCode((int)response.StatusCode, "Error occurred while creating plant.");
                    }
                }
            }
            catch (Exception ex)
            {

                return StatusCode((500), ex.Message);
            }

        }
    }
}
