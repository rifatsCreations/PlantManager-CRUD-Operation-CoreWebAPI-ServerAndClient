using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Server.Models;
using Server.Models.DTOs;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantsController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _web;

        public PlantsController(AppDbContext db, IWebHostEnvironment web)
        {
            _db = db;
            _web = web;
        }

        [HttpGet]
        public IActionResult GetPlants()
        {
            List<Plant> plants = _db.Plants.Include(pc => pc.PlantCareTypes).ToList();
            string jsonString = JsonConvert.SerializeObject(plants, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            });
            return Content(jsonString, "application/json");
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployee(int id)
        {
            Plant plant = _db.Plants.Include(pc => pc.PlantCareTypes).SingleOrDefault(e => e.PlantId == id);
            if (id == null)
            {
                return NotFound();
            }
            string jsonString = JsonConvert.SerializeObject(plant, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            });
            return Content(jsonString, "application/json");
        }

        [HttpPost]
        public async Task<IActionResult> PostPlant([FromForm] Common objCommon)
        {
            ImageUpload fileApi = new ImageUpload();
            //string fileName = objCommon.ImageName + ".jpg";
            string fileName = objCommon.ImageName;
            fileApi.ImgName = "\\images\\" + fileName;
            if (objCommon.ImageFile?.Length > 0)
            {
                if (!Directory.Exists(_web.WebRootPath + "\\images"))
                {
                    Directory.CreateDirectory(_web.WebRootPath + "\\images\\");
                }
                string filePath = _web.WebRootPath + "\\images\\" + fileName;
                using (FileStream fileStream = System.IO.File.Create(filePath))
                {
                    objCommon.ImageFile.CopyTo(fileStream);
                    fileStream.Flush();
                }
                fileApi.ImgName = "/images/" + fileName;
            }
            Plant plantObj = new Plant();
            plantObj.PlantName = objCommon.PlantName;
            plantObj.Date = objCommon.Date;
            plantObj.IsFlowerBearing = objCommon.IsFlowerBearing;
            plantObj.PlantPrice = objCommon.PlantPrice;
            plantObj.ImageName = fileApi.ImgName;
            plantObj.ImageUrl = fileApi.ImgName;


            List<PlantCareType> plantCareList = JsonConvert.DeserializeObject<List<PlantCareType>>(objCommon.PlantCareTypes!);
            plantObj.PlantCareTypes = plantCareList;
            _db.Plants.Add(plantObj);
            await _db.SaveChangesAsync();

            return Ok(plantObj);
        }

        [HttpPut()]
        public async Task<IActionResult> PutPlant(int id, [FromForm] Common objCommon)
        {
            var plantObj = await _db.Plants.FindAsync(id);
            if (plantObj == null) return NotFound("Plant not found");
            ImageUpload fileApi = new ImageUpload();
            string fileName = objCommon.ImageName + ".jpg";
            fileApi.ImgName = "\\images\\" + fileName;
            if (objCommon.ImageFile?.Length > 0)
            {
                if (!Directory.Exists(_web.WebRootPath + "\\images"))
                {
                    Directory.CreateDirectory(_web.WebRootPath + "\\images\\");
                }
                string filePath = _web.WebRootPath + "\\images\\" + fileName;
                using (FileStream fileStream = System.IO.File.Create(filePath))
                {
                    objCommon.ImageFile.CopyTo(fileStream);
                    fileStream.Flush();
                }
                fileApi.ImgName = "/images/" + fileName;
            }
            plantObj.PlantName = objCommon.PlantName;
            plantObj.Date = objCommon.Date;
            plantObj.IsFlowerBearing = objCommon.IsFlowerBearing;
            plantObj.ImageName = fileApi.ImgName;
            plantObj.ImageUrl = fileApi.ImgName;
            List<PlantCareType> plantCareList = JsonConvert.DeserializeObject<List<PlantCareType>>(objCommon.PlantCareTypes!);
            var existingCares = _db.PlantCareTypes.Where(p => p.PlantId == id);
            _db.PlantCareTypes.RemoveRange(existingCares);

            if (plantCareList.Any())
            {
                foreach (var item in plantCareList)
                {
                    PlantCareType pct = new PlantCareType
                    {
                        PlantId = plantObj.PlantId,
                        InsecticideName = item.InsecticideName,
                        Fertilizer = item.Fertilizer
                    };
                    _db.PlantCareTypes.Add(pct);

                }
                await _db.SaveChangesAsync();
            }

            return Ok("Plant Updated successfully");
        }

        [HttpDelete()]
        public async Task<IActionResult> DeletePlant(int id)
        {
            var plantObj = await _db.Plants.FindAsync(id);
            if (plantObj == null) return NotFound("Plant not found");
            _db.Plants.Remove(plantObj);
            await _db.SaveChangesAsync();
            return Ok("Plant deleted successfully");
        }

    }
}
