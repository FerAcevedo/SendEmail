using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using API.DTOs;
using FluentEmail.Core;
using FluentEmail.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class EmailSenderController : ControllerBase
    {

        [HttpPost("emailsend")]
        public async Task<ActionResult> SendSingleEmail([FromServices] IFluentEmail singleEmail, EmailDto emailDto)
        {    
            MemoryStream stream = new();
            XmlSerializer serializer = new(emailDto.GetType());
            serializer.Serialize(stream, emailDto);
            stream.Seek(0, SeekOrigin.Begin);

            var email = singleEmail
                .To(emailDto.EmailAddress)
                .Subject($"Text to translate with Ref #: {emailDto.RefNumber}")
                .Body($"Hello, {Environment.NewLine} This email contains the file to translate with the reference number: {emailDto.RefNumber}")
                .Attach(new Attachment
                {
                    Data = stream,
                    ContentType = "text/xml",
                    Filename = $"Translate_RefNo{emailDto.RefNumber}.xml"
                });

            await email.SendAsync();
            
            return Ok();
        }
    }
}