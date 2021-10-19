using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WordCount.Core.Resources;
using WordCount.Core.Services;
using WordCountSolution.Validations;

namespace WordCountSolution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TextController : ControllerBase
    {
        private readonly ITextService _textService;

        public TextController(ITextService textService)
        {
            _textService = textService;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<TextResource>>> GetAllTexts()
        {
            var textResources = await _textService.GetAllTexts();
            return Ok(textResources);
        }

        [HttpPost("typing")]
        public async Task<ActionResult<TextResource>> CreateTextTyping([FromBody] SaveTextResource saveTextResource)
        {
            var validation = new SaveTextResourceValidator();
            var validationResult = await validation.ValidateAsync(saveTextResource);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            return Ok(_textService.CreateTextTyping(saveTextResource));
        }

        [HttpPost("db/{textId:int}")]
        public async Task<ActionResult<TextResource>> CreateTextDb(int textId)
        {
            var textCreated = await _textService.CreateTextDb(textId);
            if (textCreated == null)
                return BadRequest("Try different id.");

            return Ok(textCreated);
        }

        [HttpPost("file/{path}")]
        public async Task<ActionResult<TextResource>> CreateTextFile(string path)
        {
            var textCreated = await _textService.CreateTextFile(path);
            if (textCreated == null)
                return BadRequest("Try different path.");

            return Ok(textCreated);
        }
    }
}