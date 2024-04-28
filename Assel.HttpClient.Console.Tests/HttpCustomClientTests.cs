using FluentAssertions;
using Moq;
using Moq.Protected;
using System.Net;

namespace Assel.University.Console.Tests
{
    public class HttpCustomClientTests
    {
        [Test]
        public void Should_Return_Universities()
        {
            // Arrange
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent("[{\"name\": \"Middlesbrough College\", \"state-province\": null, \"domains\": [\"middlesbro.ac.uk\", \"mbro.ac.uk\"], \"web_pages\": [\"https://www.mbro.ac.uk/\"], \"alpha_two_code\": \"GB\", \"country\": \"United Kingdom\"}, {\"name\": \"Middlesex Community College\", \"state-province\": null, \"domains\": [\"middlesex.mass.edu\"], \"web_pages\": [\"http://www.middlesex.mass.edu\"], \"alpha_two_code\": \"US\", \"country\": \"United States\"}, {\"name\": \"Middlesex County College\", \"state-province\": null, \"domains\": [\"middlesexcc.edu\"], \"web_pages\": [\"http://www.middlesexcc.edu\"], \"alpha_two_code\": \"US\", \"country\": \"United States\"}, {\"name\": \"Middlesex University - London\", \"state-province\": null, \"domains\": [\"mdx.ac.uk\"], \"web_pages\": [\"https://www.mdx.ac.uk/\"], \"alpha_two_code\": \"GB\", \"country\": \"United Kingdom\"}, {\"name\": \"Middle Georgia State College\", \"state-province\": null, \"domains\": [\"mga.edu\"], \"web_pages\": [\"http://www.mga.edu/\"], \"alpha_two_code\": \"US\", \"country\": \"United States\"}, {\"name\": \"Middle Tennessee State University\", \"state-province\": null, \"domains\": [\"mtsu.edu\"], \"web_pages\": [\"http://www.mtsu.edu/\"], \"alpha_two_code\": \"US\", \"country\": \"United States\"}, {\"name\": \"Middle East University\", \"state-province\": null, \"domains\": [\"meu.edu.jo\"], \"web_pages\": [\"http://www.meu.edu.jo/\"], \"alpha_two_code\": \"JO\", \"country\": \"Jordan\"}, {\"name\": \"Middle East Technical University\", \"state-province\": null, \"domains\": [\"metu.edu.tr\"], \"web_pages\": [\"http://www.metu.edu.tr/\"], \"alpha_two_code\": \"TR\", \"country\": \"Turkey\"}, {\"name\": \"American University of Middle East\", \"state-province\": null, \"domains\": [\"aum.edu.kw\"], \"web_pages\": [\"http://www.aum.edu.kw/\"], \"alpha_two_code\": \"KW\", \"country\": \"Kuwait\"}, {\"name\": \"Middlebury College\", \"state-province\": null, \"domains\": [\"middlebury.edu\"], \"web_pages\": [\"http://www.middlebury.edu/\"], \"alpha_two_code\": \"US\", \"country\": \"United States\"}]"),
               })
               .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.test/"),
            };

            var customClient = new HttpCustomClient();
            customClient.Client = httpClient;

            // Act
            var result = customClient.GetData().Result;

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().Be(true);
            result.Value.Count.Should().Be(2);
        }

        [Test]
        public void Should_Failed_HttpClient_Return_Request_Forbidden()
        {
            // Arrange
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.Forbidden,
               })
               .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.test/"),
            };

            var customClient = new HttpCustomClient();
            customClient.Client = httpClient;

            // Act
            var result = customClient.GetData().Result;

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().Be(false);
        }

        [Test]
        public void Return_University_For_Missing_Fields()
        {
            // Arrange
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent("[{\"data\": \"Middlesbrough College\",\"country\": \"United Kingdom\"}]"),
               })
               .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.test/"),
            };

            var customClient = new HttpCustomClient();
            customClient.Client = httpClient;

            // Act
            var result = customClient.GetData().Result;

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().Be(true);
            result.Value.Count.Should().Be(1);
        }
    }
}