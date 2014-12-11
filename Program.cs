using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace Monty_Python_Script{

    class character{
        string name;
        int lines;
        int words;
        public character(string charac){
            name = charac;
            lines = 0;
            words = 0; 
        }
        public string getName(){
            return name; 
        }
        public int getNumOfLines(){
            return lines;
        }
        public int getNumOfWords(){
            return words; 
        }
        /*public static bool operator ==(character a, character b){
            if (a.name == b.name){
                return true;
            }
            return false; 
        } */
    }

   public class scene{
        List<character> characters;
        int numOfStageDirections;
        int totalLines;
        int totalWords;
        string sceneNum;
        public scene(string sceneNum){
            numOfStageDirections = 0;
            totalLines = 0;
            this.sceneNum = sceneNum;
        }
        public int getTotalLines(){
            return totalLines;
        }
        public int getTotalWords(){
            return totalWords;
        }
        public int getNumOfStageDirections(){
            return numOfStageDirections;
        }
        public void setNumOfStageDirections(){
            numOfStageDirections++;
        }
    }

    class Program{
        static void Main(string[] args){

            /* 
                Getting html info from the website.  
             */
            WebClient client = new WebClient();
            string data = client.DownloadString("http://www.sacred-texts.com/neu/mphg/mphg.htm");
                      
            
            /*
                Putting the data from the website into a text file.
             */
            string pathToTxtWithData = "C:\\Users\\Grace\\Documents\\Visual Studio 2013\\Projects\\Monty Python Script\\theScript.txt";
            File.WriteAllText(pathToTxtWithData, data);

            List<scene> scenesInMontyHall = new List<scene>(); 

            /*
                Reading from the file one line at a time.
             */
            char[] delimiters = { '<', '>', ':'};
            string sceneName; 
            string scriptLine;
            int counter = -1;
            bool newScene = false; 
            StreamReader file = new StreamReader(pathToTxtWithData);
            while ((scriptLine = file.ReadLine()) != null){
               if (scriptLine.Contains("<H4>Scene")){
                   newScene = true;
                   string[] words = scriptLine.Split(delimiters);
                   sceneName = words[2];
                   scene temp = new scene(sceneName);
                   scenesInMontyHall.Add(temp);
                   counter++; 
               }
               else if (scriptLine.Contains("</PRE>")){
                   newScene = false;
               }

               if ((scriptLine.Contains('[') || scriptLine.Contains('(')) && newScene==true ){
                   scenesInMontyHall[counter].setNumOfStageDirections();
               }
              
               
    
            }

            Console.Read(); 
        }
    }
}
