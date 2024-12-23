# Autenticação no Swagger com Login e Senha

Este projeto demonstra como implementar autenticação com login e senha para acessar o Swagger em uma aplicação API REST construída com C# e .NET 6.0.

## 🚀 Funcionalidade

A funcionalidade de autenticação restringe o acesso à interface do Swagger, permitindo apenas usuários autenticados.

---

## 📋 Etapas de Implementação

### 1️⃣ Passo 1: Configurar as Views de Autenticação

#### 1.1 Criar o arquivo `SwaggerAutenticacaoView.html`
- Este é um arquivo HTML simples que fornece uma interface gráfica para autenticação de usuários.
- Ele permite que os usuários insiram suas credenciais (nome de usuário e senha) para realizar o login antes de acessar o Swagger.

#### 1.2 Criar o arquivo `SwaggerAutenticacaoErrorView.html`
- Este arquivo exibe uma mensagem de erro caso as credenciais fornecidas sejam inválidas.
- Após a mensagem de erro, o usuário é redirecionado para a tela de autenticação.

---

### 2️⃣ Passo 2: Configurar a Restrição de Acesso ao Swagger

#### 2.1 Criar a classe `ConfigureAuthorizationSwagger`
- Essa classe tem o objetivo de restringir o acesso ao Swagger.
- Apenas usuários com as credenciais pré-definidas podem acessar a interface do Swagger.
- Em caso de erro, o usuário é redirecionado para a tela de autenticação com uma mensagem apropriada.

---

### 3️⃣ Passo 3: Configurar o Middleware e Sessões

#### 3.1 Configuração na classe `Program` para sessões

Adicione o seguinte código no método `Program` para configurar o cache e sessões:

```csharp
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

app.UseSession();
app.UseMiddleware<ConfigureAuthorizationSwagger>();



