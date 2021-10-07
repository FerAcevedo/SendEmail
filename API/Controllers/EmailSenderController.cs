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
            StreamWriter sw = new(stream, Encoding.UTF8);
            await sw.WriteAsync(SerializeToXmlDocument(emailDto));
            await sw.FlushAsync();
            stream.Seek(0, SeekOrigin.Begin);

            var email = singleEmail
                .To(emailDto.EmailAddress)
                .Subject($"Text to translate with Ref #: {emailDto.RefNumber}")
                .Body("This is a single email")
                .Attach(new Attachment
                {
                    Data = stream,
                    ContentType = "text/plain",
                    Filename = $"Translate_RefNo{emailDto.RefNumber}.xml"
                });

            await email.SendAsync();
            
            return Ok();
        }

        public string SerializeToXmlDocument(EmailDto emailDto)
        {
            XmlSerializer serializer = new(emailDto.GetType(), "http://schemas.translatedoc.com");
            using MemoryStream stream = new();
            serializer.Serialize(stream, emailDto);
            stream.Position = 0;
            using XmlReader reader = XmlReader.Create(stream, new XmlReaderSettings{ IgnoreWhitespace = true });
            XmlDocument xmlDocument = new();
            xmlDocument.Load(reader);

            return xmlDocument.ToString();
        }
    }
}