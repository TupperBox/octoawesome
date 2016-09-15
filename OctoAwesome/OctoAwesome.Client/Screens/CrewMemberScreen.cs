﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonoGameUi;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OctoAwesome.Client.Components;
using System.Diagnostics;

namespace OctoAwesome.Client.Screens
{
    class CrewMemberScreen : BaseScreen
    {
        public CrewMemberScreen(ScreenComponent manager, CrewMember member) : base(manager)
        {
            VerticalAlignment = VerticalAlignment.Stretch;
            HorizontalAlignment = HorizontalAlignment.Stretch;

            Title = Languages.OctoClient.CreditsCrew + ": " + member.Username;

            SpriteFont boldFont = manager.Content.Load<SpriteFont>("BoldFont");

            Padding = new Border(0, 0, 0, 0);

            SetDefaultBackground();

            //The Panel
            Texture2D panelBackground = manager.Content.LoadTexture2DFromFile("./Assets/OctoAwesome.Client/panel.png", manager.GraphicsDevice);
            Panel panel = new Panel(manager)
            {
                MaxWidth = 750,                
                Background = NineTileBrush.FromSingleTexture(panelBackground, 30, 30),
                Padding = new Border(15, 15, 15, 15),
            };
            Controls.Add(panel);

            //The Main Stack - Split the Panel in half Horizontal
            StackPanel horizontalStack = new StackPanel(manager)
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                Orientation = Orientation.Horizontal
            };
            panel.Controls.Add(horizontalStack);


            //The Profile Image
            Image profileImage = new Image(manager)
            {
                Height = 200,
                Width = 200,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Padding = new Border(0, 0, 10, 0)
            };
            if (member.PictureFilename == null)
                profileImage.Texture = manager.Content.LoadTexture2DFromFile("./Assets/OctoAwesome.Client/Crew/base.png", manager.GraphicsDevice);
            else profileImage.Texture = manager.Content.LoadTexture2DFromFile(member.PictureFilename, manager.GraphicsDevice);
            horizontalStack.Controls.Add(profileImage);

            //The Text Stack
            StackPanel textStack = new StackPanel(manager);
            textStack.VerticalAlignment = VerticalAlignment.Stretch;
            textStack.HorizontalAlignment = HorizontalAlignment.Left;
            textStack.Width = 430;
            horizontalStack.Controls.Add(textStack);

            //The Username
            Label username = new Label(manager)
            {
                Text = member.Username,
                Font = manager.Content.Load<SpriteFont>("HeadlineFont"),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top
            };
            textStack.Controls.Add(username);

            //The Alias
            Label alias = new Label(manager)
            {
                Text = member.Alias,
                HorizontalAlignment = HorizontalAlignment.Left
            };
            textStack.Controls.Add(alias);

            //Achievements
            string achievementString = string.Join(", ", member.AchievementList.Select(a => a.ToString()));

            StackPanel achievementStack = new StackPanel(manager)
            {
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
                Orientation = Orientation.Horizontal,
            };
            textStack.Controls.Add(achievementStack);

            Label achievementsTitle = new Label(manager) { Text = Languages.OctoClient.Achievements + ": ", Font = boldFont, HorizontalAlignment = HorizontalAlignment.Left };
            achievementStack.Controls.Add(achievementsTitle);
            Label achievements = new Label(manager) { Text = achievementString, HorizontalAlignment = HorizontalAlignment.Left };            
            achievementStack.Controls.Add(achievements);

            // Links
            string linkString = string.Join(", ", member.Links.Select(a => a.Title));

            StackPanel linkStack = new StackPanel(manager)
            {
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
                Orientation = Orientation.Horizontal,
            };
            textStack.Controls.Add(linkStack);

            Label linkTitle = new Label(manager) { Text = Languages.OctoClient.Links + ": ", Font = boldFont, HorizontalAlignment = HorizontalAlignment.Left };
            linkStack.Controls.Add(linkTitle);

            foreach (var link in member.Links)
            {                
                if (CheckHttpUrl(link.Url))
                {
                    Button linkButton = Button.TextButton(manager, link.Title);
                    linkButton.LeftMouseClick += (s, e) => Process.Start(link.Url);
                    linkStack.Controls.Add(linkButton);
                }
            }

            Panel descriptionPanel = new Panel(manager)
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
            };
            textStack.Controls.Add(descriptionPanel);

            Label description = new Label(manager)
            {
                Text = member.Description,
                WordWrap = true,
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Left,
                HorizontalTextAlignment = HorizontalAlignment.Left,
                VerticalTextAlignment = VerticalAlignment.Top,
            };
            description.InvalidateDimensions();
            descriptionPanel.Controls.Add(description);

            panel.Width = 700;
        }

        private bool CheckHttpUrl(string url)
        {
            Uri tmp;            
            return Uri.TryCreate(url, UriKind.Absolute, out tmp) && (tmp.Scheme == Uri.UriSchemeHttp || tmp.Scheme == Uri.UriSchemeHttps);
        }
    }
}
