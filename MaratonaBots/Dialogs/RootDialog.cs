using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace MaratonaBots.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            //mark down
            await context.PostAsync("**Olá, tudo bom**");

            var message = activity.CreateReply();

            if (activity.Text.Equals("herocard", StringComparison.InvariantCultureIgnoreCase))
            {
                var heroCard = new HeroCard();

                heroCard.Title = "Planeta";
                heroCard.Subtitle = "Universo";

                heroCard.Images = new List<CardImage>
                {
                    new CardImage("http://caminhosdailuminacao.com.br/wp-content/uploads/2016/04/4-Coisas-que-Fazem-com-que-o-Planeta-Terra-seja-um-Mundo-Inferior.jpg", 
                    "Planeta", new CardAction(ActionTypes.OpenUrl, "Microsoft", value: "https://www.microsoft.com"))
                };

                heroCard.Buttons = new List<CardAction>
                {
                    new CardAction
                    {
                        Text = "Botão 1",
                        DisplayText = "Display",
                        Title = "Título",
                        Type = ActionTypes.PostBack,
                        Value = "aqui vai um valor"
                    },

                    new CardAction
                    {
                        Text = "Botão 2",
                        DisplayText = "Display 2",
                        Title = "Título 2",
                        Type = ActionTypes.PostBack,
                        Value = "aqui vai um valor 2"
                    }
                };

                message.Attachments.Add(heroCard.ToAttachment());
            } else if (activity.Text.Equals("videocard", StringComparison.InvariantCultureIgnoreCase))
            {
                var videoCard = new VideoCard();

                videoCard.Title = "Paraíso";
                videoCard.Subtitle = "Um local que é um paraíso";
                videoCard.Autostart = true;
                videoCard.Autoloop = false;
                videoCard.Media = new List<MediaUrl>
                {
                    new MediaUrl("https://www.youtube.com/watch?v=WFuIgefNO5Y")
                };

                message.Attachments.Add(videoCard.ToAttachment());
            }
            else if (activity.Text.Equals("audiocard", StringComparison.InvariantCultureIgnoreCase))
            {
                //var audioCard = new AudioCard();

                //audioCard.Title = "Paraíso";
                //audioCard.Subtitle = "Um local que é um paraíso";
                //audioCard.Image = new ThumbnailUrl("http://caminhosdailuminacao.com.br/wp-content/uploads/2016/04/4-Coisas-que-Fazem-com-que-o-Planeta-Terra-seja-um-Mundo-Inferior.jpg", "Planeta Audio");
                //audioCard.Autostart = true;
                //audioCard.Autoloop = false;
                //audioCard.Media = new List<MediaUrl>
                //    {
                //        new MediaUrl("http://www.wavlist.com/movies/004/father.wav")
                //    };

                //message.Attachments.Add(audioCard.ToAttachment());

                var attachment = CreateAudioCard();
                message.Attachments.Add(attachment);

            }else if(activity.Text.Equals("animationcard", StringComparison.InvariantCultureIgnoreCase))
            {
                //    var animationCard = new AnimationCard();

                //    animationCard.Title = "Gif bonitinho";
                //    animationCard.Subtitle = "animation card";
                //    animationCard.Autostart = true;
                //    animationCard.Autoloop = false;
                //    animationCard.Media = new List<MediaUrl>
                //    {
                //        new MediaUrl("http://www.gifs.blog.br/imagens/gifs-imagem-gif-animado-1.gif")
                //    };

                //    message.Attachments.Add(animationCard.ToAttachment());

                var attachment = CreateAnimationCard();
                message.Attachments.Add(attachment);

            }else if (activity.Text.Equals("carousel", StringComparison.InvariantCultureIgnoreCase))
            {
                message.AttachmentLayout = AttachmentLayoutTypes.Carousel;

                var audio = CreateAudioCard();
                var animation = CreateAnimationCard();

                message.Attachments.Add(audio);
                message.Attachments.Add(animation);
            }else
            {
                // calculate something for us to return
                int length = (activity.Text ?? string.Empty).Length;

                // return our reply to the user
                if (activity.Locale == "pt-BR")
                {
                    await context.PostAsync($"Você enviou um '{activity.Text}' que tem {length} caracteres");
                }
                else
                {
                    await context.PostAsync($"You sent {activity.Text} which was {length} characters");
                }
            }

            await context.PostAsync(message);
            context.Wait(MessageReceivedAsync);
        }

        //adaptivecards.io

        private Attachment CreateAnimationCard()
        {
            var animationCard = new AnimationCard();

            animationCard.Title = "Gif bonitinho";
            animationCard.Subtitle = "animation card";
            animationCard.Autostart = true;
            animationCard.Autoloop = false;
            animationCard.Media = new List<MediaUrl>
                {
                    new MediaUrl("http://www.gifs.blog.br/imagens/gifs-imagem-gif-animado-1.gif")
                };

            return animationCard.ToAttachment();
        }

        private Attachment CreateAudioCard()
        {
            var audioCard = new AudioCard();

            audioCard.Title = "Paraíso";
            audioCard.Subtitle = "Um local que é um paraíso";
            audioCard.Image = new ThumbnailUrl("http://caminhosdailuminacao.com.br/wp-content/uploads/2016/04/4-Coisas-que-Fazem-com-que-o-Planeta-Terra-seja-um-Mundo-Inferior.jpg", "Planeta Audio");
            audioCard.Autostart = true;
            audioCard.Autoloop = false;
            audioCard.Media = new List<MediaUrl>
                    {
                        new MediaUrl("http://www.wavlist.com/movies/004/father.wav")
                    };

            return audioCard.ToAttachment();
        }

    }
}