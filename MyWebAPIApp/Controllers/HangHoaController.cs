using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebAPIApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebAPIApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangHoaController : ControllerBase
    {
        public static List<HangHoa> hangHoas = new List<HangHoa>();

        //Lay Danh Sach Hang Hoa
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(hangHoas);
        }

        //Tim Kiem Hang Hoa
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            try
            {
                //Linq query
                var findHangHoa = hangHoas.SingleOrDefault(hh => hh.MaHangHoa == Guid.Parse(id));
                if (findHangHoa == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(findHangHoa);
                }
            }
            catch (Exception)
            {

                return BadRequest();
            }
           
        }

        //Them Moi Hang Hoa
        [HttpPost]
        public IActionResult Create(HangHoaVM hangHoaVM)
        {
            var hanghoa = new HangHoa
            {
                MaHangHoa=Guid.NewGuid(),
                TenHangHoa=hangHoaVM.TenHangHoa,
                DonGia=hangHoaVM.DonGia
            };
            hangHoas.Add(hanghoa);
            return Ok(new
            {
                Success = true,
                Data = hanghoa
            });
        }

        //Sua Hang Hoa
        [HttpPut("{id}")]
        public IActionResult Edit(string id, HangHoa hangHoaEdit)
        {
            try
            {
                //Linq query
                var findHangHoa = hangHoas.SingleOrDefault(hh => hh.MaHangHoa == Guid.Parse(id));
                if (findHangHoa == null)
                {
                    return NotFound();
                }
                else
                {
                    if (id!=findHangHoa.MaHangHoa.ToString())
                    {
                        return BadRequest();
                    }
                    //Update
                    findHangHoa.TenHangHoa = hangHoaEdit.TenHangHoa;
                    findHangHoa.DonGia = hangHoaEdit.DonGia;
                    return Ok();
                }
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
        //Xoa Hang Hoa
        [HttpDelete("{id}")]
        public IActionResult Delete(string id, HangHoa hangHoaDelete)
        {
            try
            {
                //Linq query
                var findHangHoa = hangHoas.SingleOrDefault(hh => hh.MaHangHoa == Guid.Parse(id));
                if (findHangHoa == null)
                {
                    return NotFound();
                }
                else
                {
                    if (id != findHangHoa.MaHangHoa.ToString())
                    {
                        return BadRequest();
                    }
                    //Delete
                    hangHoas.Remove(findHangHoa);
                    return Ok();
                }
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
    }
}
