using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using OwnerAPI.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OwnerAPI.Controllers
{
    [Route("api/[controller]")]
    public class OwnerController : Controller
    {

        public List<Owner> OwnerList = new List<Owner>
            
            
            {
                new Owner
                {
                    Id = 1,
                    Name = "John Doe",
                    Description = "hjjkjkj",
                    Date="December 2022",
                    Type="K"
                },

                new Owner
                {
                    Id = 2,
                    Name = "Jane Doe",
                    Description = "jjjjj",
                    Date="November 2022",
                    Type = "N"
                },

                new Owner
                {
                    Id = 3,
                    Name = "James Doe",
                    Description = "jjjjj",
                    Date= "February 2021",
                    Type = "M"
                   
                }

            };



        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateOwner([FromBody] Owner owner)

        {
           List<Owner> Owner_List = OwnerList;

            if (owner == null)
                return BadRequest();


            if (owner.Description.Contains("hack"))
                return BadRequest();

            else
                OwnerList.Add(owner);
                return Created("", owner);
        }


       


        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            List<Owner> Owner_List = OwnerList;

            var owner = Owner_List.FirstOrDefault(x => x.Id == id);

            if (owner != null)
            {
                Owner_List.Remove(owner);
                return Ok(Owner_List);
            }

           
           return NotFound($"There is now owner with the ID of {id}");

            
        }



        [HttpPut("{id:int}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateOwner(int id,Owner newData)
        {

           
            List<Owner> Owner_List = OwnerList;

            var update = Owner_List.FirstOrDefault(x => x.Id == id);

            if (update!=null)
            {
                if (id==newData.Id)
                {
                    update.Name = newData.Name.ToUpper();
                    update.Description = newData.Description.ToLower();
                    update.Type = newData.Type;
                    update.Date = newData.Date.ToUpper();

                    return Ok(update);
                }


                return BadRequest("You can not change the Id of a owner");

            }

            return NotFound($"There is now owner with the ID of {id}");




        }




      
        [HttpGet]
        [Route("All")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult GetAllList()

        {

            List<Owner> Owner_List = OwnerList;


            if (!Owner_List.Any())

                return NotFound("No Owner");

            return Ok(Owner_List);

        }



     
        [HttpGet]
        [Route("Query")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult GetOwnersWithListNumber([FromQuery] int count)

        {
            List<Owner> Owner_List = OwnerList;

            if (Owner_List.Any())

                return Ok(Owner_List.Take(count));
            else
                return NotFound("No Owner");

        }






    }
}

