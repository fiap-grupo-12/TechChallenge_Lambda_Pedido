using Amazon.SQS;
using Amazon.SQS.Model;
using FIAP.TechChallenge.LambdaPedido.Domain.Repositories;

namespace FIAP.TechChallenge.LambdaPedido.Infra.Message
{
    public class MensageriaSolicitaPagamento : IMensageriaSolicitaPagamento
    {
        private readonly IAmazonSQS _amazonSQS;
        private readonly string _url;

        public MensageriaSolicitaPagamento(IAmazonSQS amazonSQS)
        {
            _amazonSQS = amazonSQS;
            _url = Environment.GetEnvironmentVariable("url_sqs_solicita_pagamento");
        }

        public async Task SendMessage(string body)
        {
            var message = new SendMessageRequest()
            {
                QueueUrl = _url,
                MessageBody = body
            };
            try
            {
                await _amazonSQS.SendMessageAsync(message);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao enviar a mensagem: {ex}");
            }
        }
    }
}
