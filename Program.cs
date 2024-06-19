using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChipSecuritySystem
{
    class Program
    {
        static void Main(string[] args)
        {
            List<ColorChip> chipsBag = new List<ColorChip>();

            //chipsBag.Add(new ColorChip(Color.Blue, Color.Yellow));
            //chipsBag.Add(new ColorChip(Color.Red, Color.Green));
            //chipsBag.Add(new ColorChip(Color.Yellow, Color.Red));
            //chipsBag.Add(new ColorChip(Color.Orange, Color.Purple));


            //chipsBag.Add(new ColorChip(Color.Blue, Color.Purple));
            //chipsBag.Add(new ColorChip(Color.Blue, Color.Purple));
            //chipsBag.Add(new ColorChip(Color.Blue, Color.Purple));
            //chipsBag.Add(new ColorChip(Color.Purple, Color.Purple));
            //chipsBag.Add(new ColorChip(Color.Purple, Color.Green));
            //chipsBag.Add(new ColorChip(Color.Green, Color.Green));
            //chipsBag.Add(new ColorChip(Color.Green, Color.Red));
            //chipsBag.Add(new ColorChip(Color.Blue, Color.Yellow));
            //chipsBag.Add(new ColorChip(Color.Yellow, Color.Red));
            //chipsBag.Add(new ColorChip(Color.Red, Color.Green));
            //chipsBag.Add(new ColorChip(Color.Yellow, Color.Blue));
            //chipsBag.Add(new ColorChip(Color.Orange, Color.Purple));

            ValidChannelWithMostChips(chipsBag);

            Console.WriteLine("\n\nPress any key to continue...");
            Console.ReadKey();
        }

        static void ValidChannelWithMostChips(List<ColorChip> chipsBag)
        {
            try
            {
                List<List<ColorChip>> channelsEndingInGreen = new List<List<ColorChip>>();
                List<List<ColorChip>> channels = new List<List<ColorChip>>();
                List<List<ColorChip>> channelsTemp = new List<List<ColorChip>>();

                List<ColorChip> chipsBagTemp = new List<ColorChip>();

                var filteredChips = from chips in chipsBag
                                    where chips.StartColor == Color.Blue
                                    select chips;

                if (filteredChips.FirstOrDefault() == null)
                {
                    Console.WriteLine(Constants.ErrorMessage);
                    return;
                }

                foreach (var chips in filteredChips.ToList())
                {
                    List<ColorChip> chipTemp = new List<ColorChip>();
                    chipTemp.Add(new ColorChip(chips.StartColor, chips.EndColor));
                    channels.Add(chipTemp);
                }

                bool existAdjacents = true;

                channelsTemp = new List<List<ColorChip>>(channels);
                while (existAdjacents)
                {
                    existAdjacents = false;

                    channelsTemp.Clear();
                    channelsTemp = null;
                    channelsTemp = new List<List<ColorChip>>();

                    channelsEndingInGreen = new List<List<ColorChip>>();

                    foreach (List<ColorChip> channel in channels)
                    {
                        chipsBagTemp.Clear();
                        chipsBagTemp = null;
                        chipsBagTemp = new List<ColorChip>(chipsBag);

                        foreach (ColorChip chipTemp in channel)
                        {
                            var item = chipsBagTemp.Last(x => x.StartColor == chipTemp.StartColor && x.EndColor == chipTemp.EndColor);
                            chipsBagTemp.Remove(item);
                        }

                        ColorChip chip = channel.LastOrDefault();

                        filteredChips = from chips in chipsBagTemp
                                        where chips.StartColor == chip.EndColor
                                        select chips;

                        if (filteredChips.FirstOrDefault() == null)
                        {
                            List<ColorChip> channelTemp = channel.ToList();
                            channelsTemp.Add(channelTemp);
                        }
                        else
                        {
                            existAdjacents = true;
                            foreach (var chips in filteredChips)
                            {
                                List<ColorChip> channelTemp = channel.ToList();

                                channelTemp.Add(new ColorChip(chips.StartColor, chips.EndColor));
                                channelsTemp.Add(channelTemp);
                            }
                        }

                        if (chip.EndColor == Color.Green)
                        {
                            List<ColorChip> channelTemp = channel.ToList();
                            channelsEndingInGreen.Add(channelTemp);
                        }

                    }

                    if (channelsTemp.Count > 0)
                    {
                        channels.Clear();
                        channels = null;
                        channels = new List<List<ColorChip>>(channelsTemp);
                    }
                }

                if (channelsEndingInGreen.Count > 0)
                {
                    //PRINTS THE SOLUTION WITH THE MOST NUMBER OF CHIPS:

                    List<ColorChip> channelWithMostChips = channelsEndingInGreen.OrderByDescending(x => x.Count).First();

                    Console.WriteLine("Blue");
                    foreach (ColorChip chipTemp in channelWithMostChips)
                    {
                        Console.WriteLine("[" + chipTemp.ToString() + "]");
                    }
                    Console.WriteLine("Green");
                    Console.WriteLine("\n");



                    //PRINTS ALL VALID CHIPS POSSIBILITIES THAT SUCCESSFULLY LINKS THE BLUE AND GREEN MARKERS:

                    //Console.WriteLine("ALL VALID CHIPS POSSIBILITIES THAT SUCCESSFULLY LINKS THE BLUE AND GREEN MARKERS:\n");
                    //foreach (List<ColorChip> channel in channelsEndingInGreen)
                    //{
                    //    foreach (ColorChip chipTemp in channel)
                    //    {
                    //        Console.WriteLine("[" + chipTemp.ToString() + "]");
                    //    }
                    //    Console.WriteLine("\n");
                    //}
                }
                else
                    Console.WriteLine(Constants.ErrorMessage);

            }
            catch (Exception ex)
            { 
                Console.WriteLine(ex.Message);
                Console.WriteLine(Constants.ErrorMessage);
            }
        }
    }
}
