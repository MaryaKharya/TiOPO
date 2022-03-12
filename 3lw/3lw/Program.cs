using System;
using System.Net;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace lw3
{
	public class CTVSet
	{
		private int selectedChannel = 1;
		private int previousChannel = 1;
		private bool isTurnedOn = false;
		public bool GetTurnStatus()
		{
			return isTurnedOn;
		}
		public void TurnOn()
		{
			isTurnedOn = true;
		}
		public void TurnOff()
		{
			isTurnedOn = false;
		}
		public void SelectChannel(int channel)
		{
			if (((channel >= 1) && (channel <= 99)) && (isTurnedOn))
			{
				previousChannel = selectedChannel;
				selectedChannel = channel;
			}
		}
		public void SelectPreviousChannel()
		{
			int channel;
			if (isTurnedOn)
			{
				channel = selectedChannel;
				selectedChannel = previousChannel;
				previousChannel = channel;
			}
		}
		public int GetChannel()
		{
			if (!isTurnedOn)
			{
				return 0;
			}
			return selectedChannel;
		}
	};

	class Program
	{
		static void Main(string[] args)
		{
			CTVSet tv = new CTVSet();
			string command = "new";
			while (!String.IsNullOrEmpty(command))
			{
				command = Console.ReadLine();
				if (command == "TurnOn")
				{
					tv.TurnOn();
					Console.WriteLine("TV is on");
				}
				else if (command == "TurnOff")
				{
					tv.TurnOff();
					Console.WriteLine("TV is off");
				}
				else if (command == "Info")
				{
					if (tv.GetTurnStatus())
					{
						Console.WriteLine("TV is on\nChannel number is {0}", tv.GetChannel());
					}
					else
					{
						Console.WriteLine("TV is off");
					}
				}
				else if (command == "SelectPreviousChannel")
				{
					tv.SelectPreviousChannel();
					if (tv.GetTurnStatus())
					{
						Console.WriteLine("Channel number is {0}", tv.GetChannel());
						return;
					}
					Console.WriteLine("TV is off");
				}
				else if (command.Contains("SelectChannel"))
				{
					int channel = IntToString(command);
					if (channel == -1)
					{
						Console.WriteLine("Channel number contains invalid characters");
						continue;
					}
					tv.SelectChannel(channel);
					if (tv.GetTurnStatus())
					{
						Console.WriteLine("Channel number is {0}", tv.GetChannel());
					}
					else
					{
						Console.WriteLine("TV is off");
					}
				}
				else if (command == "")
				{
					return;
				}
				else
				{
					Console.WriteLine("Command is not recognized");
				}
			}
		}
		static int IntToString(string str)
		{
			int channel = 0;
			string digits = "0123456789";
			for (int i = str.IndexOf(" ") + 1; i < str.Length; i++)
			{
				if ((str[i] < '0') || (str[i] > '9'))
				{
					return -1;
				}
				channel = channel * 10 + digits.IndexOf(str[i]);
			}
			return channel;
		}
	}
}

