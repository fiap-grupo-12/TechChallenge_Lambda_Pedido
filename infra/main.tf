provider "aws" {
  region = "us-east-1"
}

terraform {
  backend "s3" {
    bucket = "terraform-tfstate-grupo12-fiap-2024"
    key    = "lambda_pedido/terraform.tfstate"
    region = "us-east-1"
  }
}

# Definir a fila SQS
data "aws_sqs_queue" "solicita_pagamento" {
  name = "sqs_solicita_pagamento"
}

resource "aws_iam_role" "lambda_execution_role" {
  name = "lambda_pedido_execution_role"

  assume_role_policy = jsonencode({
    Version = "2012-10-17"
    Statement = [
      {
        Action = "sts:AssumeRole"
        Effect = "Allow"
        Principal = {
          Service = "lambda.amazonaws.com"
        }
      },
    ]
  })
}

resource "aws_iam_policy" "lambda_policy" {
  name        = "lambda_pedido_policy"
  description = "IAM policy for Lambda execution"
  policy = jsonencode({
    Version = "2012-10-17"
    Statement = [
      {
        Effect = "Allow"
        Action = [
          "logs:CreateLogGroup",
          "logs:CreateLogStream",
          "logs:PutLogEvents",
          "dynamodb:DeleteItem",
          "dynamodb:GetItem",
          "dynamodb:PutItem",
          "dynamodb:Query",
          "dynamodb:Scan",
          "dynamodb:UpdateItem",
          "dynamodb:DescribeTable",
          "sqs:*"
        ]
        Resource = "*"
      }
    ]
  })
}

resource "aws_iam_role_policy_attachment" "lambda_execution_policy" {
  role       = aws_iam_role.lambda_execution_role.name
  policy_arn = aws_iam_policy.lambda_policy.arn
}

resource "aws_lambda_function" "pedido_function" {
  function_name = "lambda_pedido_function"
  role          = aws_iam_role.lambda_execution_role.arn
  runtime       = "dotnet8"
  memory_size   = 512
  timeout       = 30
  handler       = "FIAP.TechChallenge.LambdaPedido.API::FIAP.TechChallenge.LambdaPedido.API.Function_Handler_Generated::Handler"
  # Código armazenado no S3
  s3_bucket = "code-lambdas-functions"
  s3_key    = "lambda_pedido_function.zip"

  environment {
    variables = {
      url_sqs_solicita_pagamento = data.aws_sqs_queue.solicita_pagamento.id
    }
  }
}

# Criação da Tabela DynamoDB
resource "aws_dynamodb_table" "pedido_table" {
  name         = "PedidoTable"
  billing_mode = "PAY_PER_REQUEST"
  hash_key     = "id"

  attribute {
    name = "id"
    type = "S" # Tipo da chave: "S" para string, "N" para número, "B" para binário
  }

  # Opcional: Definição de uma chave de classificação (range key)
  #attribute {
  #  name = "id_guid"
  #  type = "S"
  #}

  tags = {
    Team = "Grupo12TechChallenge"
  }
}
