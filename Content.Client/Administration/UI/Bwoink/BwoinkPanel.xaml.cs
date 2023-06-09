using Content.Shared.Administration;
using Robust.Client.AutoGenerated;
using Robust.Client.UserInterface.Controls;
using Robust.Client.UserInterface.XAML;
using Robust.Shared.Utility;

namespace Content.Client.Administration.UI.Bwoink
{
    [GenerateTypedNameReferences]
    public sealed partial class BwoinkPanel : BoxContainer
    {
        private readonly Action<string> _messageSender;

        public int Unread { get; private set; } = 0;
        public DateTime LastMessage { get; private set; } = DateTime.MinValue;

        public BwoinkPanel(Action<string> messageSender)
        {
            RobustXamlLoader.Load(this);

            var msg = new FormattedMessage();
            msg.PushColor(Color.LightGray);
            msg.AddText(Loc.GetString("bwoink-system-messages-being-relayed-to-discord"));
            msg.Pop();
            RelayedToDiscordLabel.SetMessage(msg);

            _messageSender = messageSender;

            OnVisibilityChanged += c =>
            {
                if (c.Visible)
                    Unread = 0;
            };
            SenderLineEdit.OnTextEntered += Input_OnTextEntered;
        }

        private void Input_OnTextEntered(LineEdit.LineEditEventArgs args)
        {
            if (string.IsNullOrWhiteSpace(args.Text))
                return;

            _messageSender.Invoke(args.Text);
            SenderLineEdit.Clear();
        }

        public void ReceiveLine(SharedBwoinkSystem.BwoinkTextMessage message)
        {
            if (!Visible)
                Unread++;

            var formatted = new FormattedMessage(1);
            formatted.AddMarkup($"[color=gray]{message.SentAt.ToShortTimeString()}[/color] {message.Text}");
            TextOutput.AddMessage(formatted);
            LastMessage = message.SentAt;
        }
    }
}
