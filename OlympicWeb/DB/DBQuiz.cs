﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using OlympicWeb.Models;
using MySql.Data.MySqlClient;

namespace OlympicWeb.DB
{

    public class DBQuiz
    {
        private MySqlConnection connection;
        private MySqlDataReader dataReader;
        private DBGeneral dbGeneral;
        private List<string> sportsList = new List<string>();
        private List<string> gamesList = new List<string>();


        //Constructor
        public DBQuiz(MySqlConnection conn, DBGeneral gen)
        {
            connection = conn;
            dbGeneral = gen;
            sportsList = dbGeneral.GetSportList();
            gamesList = dbGeneral.GetGamesList();
        }

        public List<Question> GetQuestions(string sport)
        {
            List<Question> questions = new List<Question>();
            //q1 who's the best athlete in the given sport
            List<string> theBestAthleteAnswers = dbGeneral.TheBestXAthlete(sport, " AND  medal <> \"NA\"");
            if (theBestAthleteAnswers.Count < 4)
            {
                getWorngAnswers(sport, theBestAthleteAnswers);
            }
            string question = "Who's the best athlete in the field of " + sport + "? Hint:The best athlete is the athlete who won the most medals.";
            Question q1 = new Question
            {
                QuestionString = question,
                CorrectAnswer = theBestAthleteAnswers[0],
                WrongAnswer1 = theBestAthleteAnswers[1],
                WrongAnswer2 = theBestAthleteAnswers[2],
                WrongAnswer3 = theBestAthleteAnswers[3],
                Sport = sport
            };
            questions.Add(q1);
            // q2 in which year the best athlete was born
            string birthYear = GetXByYWhereZFromAthletes("Birth_year", "Name", theBestAthleteAnswers[0]);
            List<string> wrongAnsersList = WrongYears(birthYear);
            question = "In which year " + theBestAthleteAnswers[0] + " was born?";
            Question q2 = new Question
            {
                QuestionString = question,
                CorrectAnswer = birthYear,
                WrongAnswer1 = wrongAnsersList[0],
                WrongAnswer2 = wrongAnsersList[1],
                WrongAnswer3 = wrongAnsersList[2],
                Sport = sport
            };
            questions.Add(q2);

            // q3 where this game took place
            var random = new Random();
            int index = random.Next(gamesList.Count);
            string randomGame = gamesList[index];
            string country = dbGeneral.LocationOfOlympicGame(randomGame)[0];
            wrongAnsersList = WrongCountries(country);
            question = "In which country the " + randomGame + "  games took place?";
            Question q3 = new Question
            {
                QuestionString = question,
                CorrectAnswer = country,
                WrongAnswer1 = wrongAnsersList[0],
                WrongAnswer2 = wrongAnsersList[1],
                WrongAnswer3 = wrongAnsersList[2],
                Sport = sport
            };
            questions.Add(q3);
            // q4 who is the athlete that participant the most in games
            List<string> answers = dbGeneral.TheBestXAthlete(sport, "");
            if (answers.Count < 4)
            {
                getWorngAnswers(sport, answers);
            }
            question = "Who is the athlete that participant the most in the olympic games in the field of " + sport + "?";
            Question q4 = new Question
            {
                QuestionString = question,
                CorrectAnswer = answers[0],
                WrongAnswer1 = answers[1],
                WrongAnswer2 = answers[2],
                WrongAnswer3 = answers[3],
                Sport = sport
            };
            questions.Add(q4);
            // q4 who is the tallest athlete
            List<List<string>> results = dbGeneral.TheMostXAthlete(sport, "Height", "DESC");
            if (results.Count < 4)
            {
                getWorngAnswersTallest(sport, results);
            }
            question = "Who is the tallest athlete in the field of " + sport + "?";
            Question q5 = new Question
            {
                QuestionString = question,
                CorrectAnswer = results[0][0],
                WrongAnswer1 = results[1][0],
                WrongAnswer2 = results[2][0],
                WrongAnswer3 = results[3][0],
                Sport = sport
            };
            questions.Add(q5);
            return questions;
        }

        //function gets a list of athletes and a sport
        //the function add to the list a list of athletes which arent match the question

        public void getWorngAnswers(string sport, List<string> answers)
        {
            List<string> list = Get4RandomAthletesBySport(sport);
            if (list.Count < 4)
            {
                // if there is less than 4 athletes in this sport will take some athlets from basketball
                list.AddRange(Get4RandomAthletesBySport("Basketball"));
            }
            for (int i = 0; i < list.Count; i++)
            {
                if (!answers.Contains(list[i]))
                {
                    answers.Add(list[i]);
                }
                if (answers.Count == 4)
                {
                    break;
                }
            }

        }
        //function gets a list of lists of athletes and a sport
        //the function add to the list a list of athletes which arent the tallest athletes in the sport that was given
        public void getWorngAnswersTallest(string sport, List<List<string>> answers)
        {
            List<string> list = Get4RandomAthletesBySport(sport);
            if (list.Count < 4)
            {
                list.AddRange(Get4RandomAthletesBySport("Basketball"));
            }
            for (int i = 0; i < 4; i++)
            {
                List<string> temp = new List<string>(new string[] { list[i], "0" });
                if (!answers.Contains(temp))
                {
                    answers.Add(temp);
                }
            }

        }
        //function returns a list of 4 athletes from the sport given
        public List<string> Get4RandomAthletesBySport(string sport)
        {
            var queryString = "(SELECT DISTINCT team30.athletes.Name, team30.medals.Medal " +
                              "FROM team30.medals JOIN team30.event_types " +
                              "ON team30.medals.Event_id = team30.event_types.Event_id JOIN team30.athletes " +
                              "ON team30.athletes.Athlete_id = team30.medals.Athlete_id " +
                              "WHERE team30.event_types.Sport = '" + sport + "') " +
                              "ORDER BY RAND() LIMIT 4;";
            List<string> result = new List<string>();
            try
            {
                MySqlCommand cmd = new MySqlCommand(queryString, connection);
                dataReader = cmd.ExecuteReader();
                //Read the data and store the name in string
                while (dataReader.Read())
                {
                    result.Add(dataReader["Name"] + "");
                }
            }
            catch (MySqlException) { }
            //close Data Reader
            if (dataReader != null)
            {
                dataReader.Close();
            }
            return result;
        }


        // function returns the athlete that the condition matchs
        public string GetXByYWhereZFromAthletes(string x, string y, string z)
        {
            var queryString = @"SELECT DISTINCT " + x + " From team30.athletes WHERE " + y + " = '" + z + "' LIMIT 1;";
            string result = "";
            try
            {
                MySqlCommand cmd = new MySqlCommand(queryString, connection);
                dataReader = cmd.ExecuteReader();
                //Read the data and store the name in string
                while (dataReader.Read())
                {
                    result += dataReader[x] + "";
                }
            }
            catch (MySqlException) { }
            //close Data Reader
            if (dataReader != null)
            {
                dataReader.Close();
            }
            return result;
        }

        // function returns list of 3 years without the year that was given to it
        public List<string> WrongYears(string year)
        {
            var queryString = "SELECT Birth_year From team30.athletes WHERE Birth_year <> \"" + year + "\" LIMIT 3";
            List<string> result = new List<string>();
            try
            {
                MySqlCommand cmd = new MySqlCommand(queryString, connection);
                dataReader = cmd.ExecuteReader();
                //Read the data and store the name in string
                while (dataReader.Read())
                {
                    result.Add(dataReader["Birth_year"] + "");
                }
            }
            catch (MySqlException) { }
            //close Data Reader
            if (dataReader != null)
            {
                dataReader.Close();
            }
            return result;
        }

        // function returns list of 3 countries without the country that was given to it
        public List<string> WrongCountries(string country)
        {
            var queryString = "SELECT DISTINCT Country From team30.countries WHERE Country <> \"" + country + "\" LIMIT 3;";
            List<string> result = new List<string>();
            try
            {
                MySqlCommand cmd = new MySqlCommand(queryString, connection);
                dataReader = cmd.ExecuteReader();

                //Read the data and store the name in string
                while (dataReader.Read())
                {
                    result.Add(dataReader["Country"] + "");

                }
            }
            catch (MySqlException) { }
            //close Data Reader
            if (dataReader != null)
            {
                dataReader.Close();
            }
            return result;

        }
    }
}
