using Microsoft.AspNetCore.Mvc;

namespace UsingFirebase.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Firestore : ControllerBase
    {
        [HttpGet]
        public string UsingFirestore()
        {
            return "Using Firestore";
        }
    }
}