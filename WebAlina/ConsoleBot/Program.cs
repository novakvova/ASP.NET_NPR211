// See https://aka.ms/new-console-template for more information
using System.IO;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;

var token = Environment.GetEnvironmentVariable("TOKEN") ?? "7806870757:AAG5rU-vWagmjQBl3R9Ola4lZwA0sJE7ygs";

var botClient = new TelegramBotClient(token);

botClient.StartReceiving(Update, Error);

async Task Update(ITelegramBotClient client, Update update, CancellationToken token)
{
    var message = update.Message;
    if (message == null || message.Type != MessageType.Text)
        return;

    Console.WriteLine($"{message.Chat.FirstName}({message.Chat.Id})\t|\t{message.Text}");

    if (message.Text.ToLower().Contains("/start"))
    {
        ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
            {
                new KeyboardButton[] { "Білий  🥰"},
                new KeyboardButton[] { "Чорний 😍" },
                new KeyboardButton[] { "Рижий ❤️" },
            })
        {
            ResizeKeyboard = true
        };

        Message sentMessage = await client.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: "Оберіть колір волося",
            replyMarkup: replyKeyboardMarkup,
            cancellationToken: token);
    }

    if(message.Text.Contains("🥰"))
    {
        // Path to the image you want to send
        string filePath =Path.Combine(Directory.GetCurrentDirectory(), "images", "alina.jpg");
        using (var stream = System.IO.File.OpenRead(filePath))
        {
            await client.SendPhotoAsync(message.Chat, InputFile.FromStream(stream), caption: "Read Смачно і круто");
        }    
        
    }
}

async Task Error(ITelegramBotClient client, Exception exception, CancellationToken token)
{
    throw new NotImplementedException();
}

Console.ReadLine();