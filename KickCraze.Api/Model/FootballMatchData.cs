﻿using Microsoft.ML.Data;

namespace KickCraze.Api.Model
{
    public class FootballMatchData
    {
        [LoadColumn(0)] public string HomeTeamName { get; set; }
        [LoadColumn(1)] public float HomeTeamGoalDiff { get; set; }
        [LoadColumn(2)] public float HomeTeamPosition { get; set; }
        [LoadColumn(3)] public string AwayTeamName { get; set; }
        [LoadColumn(4)] public float AwayTeamGoalDiff { get; set; }
        [LoadColumn(5)] public float AwayTeamPosition { get; set; }
        [LoadColumn(6)] public string MatchResult { get; set; }
        [LoadColumn(7)] public float H1LastHomeTeamGoalDiffBef { get; set; }
        [LoadColumn(8)] public float H1LastHomeTeamGoalDiffAft { get; set; }
        [LoadColumn(9)] public float H1LastHomeTeamPosBef { get; set; }
        [LoadColumn(10)] public float H1LastHomeTeamPosAft { get; set; }
        [LoadColumn(11)] public float H1LastHomeTeamScoreBreak { get; set; }
        [LoadColumn(12)] public float H1LastHomeTeamScore { get; set; }
        [LoadColumn(13)] public float H1LastAwayTeamGoalDiffBef { get; set; }
        [LoadColumn(14)] public float H1LastAwayTeamGoalDiffAft { get; set; }
        [LoadColumn(15)] public float H1LastAwayTeamPosBef { get; set; }
        [LoadColumn(16)] public float H1LastAwayTeamPosAft { get; set; }
        [LoadColumn(17)] public float H1LastAwayTeamScoreBreak { get; set; }
        [LoadColumn(18)] public float H1LastAwayTeamScore { get; set; }
        [LoadColumn(19)] public string H1MatchResult { get; set; }
        [LoadColumn(20)] public float H2LastHomeTeamGoalDiffBef { get; set; }
        [LoadColumn(21)] public float H2LastHomeTeamGoalDiffAft { get; set; }
        [LoadColumn(22)] public float H2LastHomeTeamPosBef { get; set; }
        [LoadColumn(23)] public float H2LastHomeTeamPosAft { get; set; }
        [LoadColumn(24)] public float H2LastHomeTeamScoreBreak { get; set; }
        [LoadColumn(25)] public float H2LastHomeTeamScore { get; set; }
        [LoadColumn(26)] public float H2LastAwayTeamGoalDiffBef { get; set; }
        [LoadColumn(27)] public float H2LastAwayTeamGoalDiffAft { get; set; }
        [LoadColumn(28)] public float H2LastAwayTeamPosBef { get; set; }
        [LoadColumn(29)] public float H2LastAwayTeamPosAft { get; set; }
        [LoadColumn(30)] public float H2LastAwayTeamScoreBreak { get; set; }
        [LoadColumn(31)] public float H2LastAwayTeamScore { get; set; }
        [LoadColumn(32)] public string H2MatchResult { get; set; }
        [LoadColumn(33)] public float H3LastHomeTeamGoalDiffBef { get; set; }
        [LoadColumn(34)] public float H3LastHomeTeamGoalDiffAft { get; set; }
        [LoadColumn(35)] public float H3LastHomeTeamPosBef { get; set; }
        [LoadColumn(36)] public float H3LastHomeTeamPosAft { get; set; }
        [LoadColumn(37)] public float H3LastHomeTeamScoreBreak { get; set; }
        [LoadColumn(38)] public float H3LastHomeTeamScore { get; set; }
        [LoadColumn(39)] public float H3LastAwayTeamGoalDiffBef { get; set; }
        [LoadColumn(40)] public float H3LastAwayTeamGoalDiffAft { get; set; }
        [LoadColumn(41)] public float H3LastAwayTeamPosBef { get; set; }
        [LoadColumn(42)] public float H3LastAwayTeamPosAft { get; set; }
        [LoadColumn(43)] public float H3LastAwayTeamScoreBreak { get; set; }
        [LoadColumn(44)] public float H3LastAwayTeamScore { get; set; }
        [LoadColumn(45)] public string H3MatchResult { get; set; }
        [LoadColumn(46)] public float H4LastHomeTeamGoalDiffBef { get; set; }
        [LoadColumn(47)] public float H4LastHomeTeamGoalDiffAft { get; set; }
        [LoadColumn(48)] public float H4LastHomeTeamPosBef { get; set; }
        [LoadColumn(49)] public float H4LastHomeTeamPosAft { get; set; }
        [LoadColumn(50)] public float H4LastHomeTeamScoreBreak { get; set; }
        [LoadColumn(51)] public float H4LastHomeTeamScore { get; set; }
        [LoadColumn(52)] public float H4LastAwayTeamGoalDiffBef { get; set; }
        [LoadColumn(53)] public float H4LastAwayTeamGoalDiffAft { get; set; }
        [LoadColumn(54)] public float H4LastAwayTeamPosBef { get; set; }
        [LoadColumn(55)] public float H4LastAwayTeamPosAft { get; set; }
        [LoadColumn(56)] public float H4LastAwayTeamScoreBreak { get; set; }
        [LoadColumn(57)] public float H4LastAwayTeamScore { get; set; }
        [LoadColumn(58)] public string H4MatchResult { get; set; }
        [LoadColumn(59)] public float H5LastHomeTeamGoalDiffBef { get; set; }
        [LoadColumn(60)] public float H5LastHomeTeamGoalDiffAft { get; set; }
        [LoadColumn(61)] public float H5LastHomeTeamPosBef { get; set; }
        [LoadColumn(62)] public float H5LastHomeTeamPosAft { get; set; }
        [LoadColumn(63)] public float H5LastHomeTeamScoreBreak { get; set; }
        [LoadColumn(64)] public float H5LastHomeTeamScore { get; set; }
        [LoadColumn(65)] public float H5LastAwayTeamGoalDiffBef { get; set; }
        [LoadColumn(66)] public float H5LastAwayTeamGoalDiffAft { get; set; }
        [LoadColumn(67)] public float H5LastAwayTeamPosBef { get; set; }
        [LoadColumn(68)] public float H5LastAwayTeamPosAft { get; set; }
        [LoadColumn(69)] public float H5LastAwayTeamScoreBreak { get; set; }
        [LoadColumn(70)] public float H5LastAwayTeamScore { get; set; }
        [LoadColumn(71)] public string H5MatchResult { get; set; }
        [LoadColumn(72)] public float A1LastHomeTeamGoalDiffBef { get; set; }
        [LoadColumn(73)] public float A1LastHomeTeamGoalDiffAft { get; set; }
        [LoadColumn(74)] public float A1LastHomeTeamPosBef { get; set; }
        [LoadColumn(75)] public float A1LastHomeTeamPosAft { get; set; }
        [LoadColumn(76)] public float A1LastHomeTeamScoreBreak { get; set; }
        [LoadColumn(77)] public float A1LastHomeTeamScore { get; set; }
        [LoadColumn(78)] public float A1LastAwayTeamGoalDiffBef { get; set; }
        [LoadColumn(79)] public float A1LastAwayTeamGoalDiffAft { get; set; }
        [LoadColumn(80)] public float A1LastAwayTeamPosBef { get; set; }
        [LoadColumn(81)] public float A1LastAwayTeamPosAft { get; set; }
        [LoadColumn(82)] public float A1LastAwayTeamScoreBreak { get; set; }
        [LoadColumn(83)] public float A1LastAwayTeamScore { get; set; }
        [LoadColumn(84)] public string A1MatchResult { get; set; }
        [LoadColumn(85)] public float A2LastHomeTeamGoalDiffBef { get; set; }
        [LoadColumn(86)] public float A2LastHomeTeamGoalDiffAft { get; set; }
        [LoadColumn(87)] public float A2LastHomeTeamPosBef { get; set; }
        [LoadColumn(88)] public float A2LastHomeTeamPosAft { get; set; }
        [LoadColumn(89)] public float A2LastHomeTeamScoreBreak { get; set; }
        [LoadColumn(90)] public float A2LastHomeTeamScore { get; set; }
        [LoadColumn(91)] public float A2LastAwayTeamGoalDiffBef { get; set; }
        [LoadColumn(92)] public float A2LastAwayTeamGoalDiffAft { get; set; }
        [LoadColumn(93)] public float A2LastAwayTeamPosBef { get; set; }
        [LoadColumn(94)] public float A2LastAwayTeamPosAft { get; set; }
        [LoadColumn(95)] public float A2LastAwayTeamScoreBreak { get; set; }
        [LoadColumn(96)] public float A2LastAwayTeamScore { get; set; }
        [LoadColumn(97)] public string A2MatchResult { get; set; }
        [LoadColumn(98)] public float A3LastHomeTeamGoalDiffBef { get; set; }
        [LoadColumn(99)] public float A3LastHomeTeamGoalDiffAft { get; set; }
        [LoadColumn(100)] public float A3LastHomeTeamPosBef { get; set; }
        [LoadColumn(101)] public float A3LastHomeTeamPosAft { get; set; }
        [LoadColumn(102)] public float A3LastHomeTeamScoreBreak { get; set; }
        [LoadColumn(103)] public float A3LastHomeTeamScore { get; set; }
        [LoadColumn(104)] public float A3LastAwayTeamGoalDiffBef { get; set; }
        [LoadColumn(105)] public float A3LastAwayTeamGoalDiffAft { get; set; }
        [LoadColumn(106)] public float A3LastAwayTeamPosBef { get; set; }
        [LoadColumn(107)] public float A3LastAwayTeamPosAft { get; set; }
        [LoadColumn(108)] public float A3LastAwayTeamScoreBreak { get; set; }
        [LoadColumn(109)] public float A3LastAwayTeamScore { get; set; }
        [LoadColumn(110)] public string A3MatchResult { get; set; }
        [LoadColumn(111)] public float A4LastHomeTeamGoalDiffBef { get; set; }
        [LoadColumn(112)] public float A4LastHomeTeamGoalDiffAft { get; set; }
        [LoadColumn(113)] public float A4LastHomeTeamPosBef { get; set; }
        [LoadColumn(114)] public float A4LastHomeTeamPosAft { get; set; }
        [LoadColumn(115)] public float A4LastHomeTeamScoreBreak { get; set; }
        [LoadColumn(116)] public float A4LastHomeTeamScore { get; set; }
        [LoadColumn(117)] public float A4LastAwayTeamGoalDiffBef { get; set; }
        [LoadColumn(118)] public float A4LastAwayTeamGoalDiffAft { get; set; }
        [LoadColumn(119)] public float A4LastAwayTeamPosBef { get; set; }
        [LoadColumn(120)] public float A4LastAwayTeamPosAft { get; set; }
        [LoadColumn(121)] public float A4LastAwayTeamScoreBreak { get; set; }
        [LoadColumn(122)] public float A4LastAwayTeamScore { get; set; }
        [LoadColumn(123)] public string A4MatchResult { get; set; }
        [LoadColumn(124)] public float A5LastHomeTeamGoalDiffBef { get; set; }
        [LoadColumn(125)] public float A5LastHomeTeamGoalDiffAft { get; set; }
        [LoadColumn(126)] public float A5LastHomeTeamPosBef { get; set; }
        [LoadColumn(127)] public float A5LastHomeTeamPosAft { get; set; }
        [LoadColumn(128)] public float A5LastHomeTeamScoreBreak { get; set; }
        [LoadColumn(129)] public float A5LastHomeTeamScore { get; set; }
        [LoadColumn(130)] public float A5LastAwayTeamGoalDiffBef { get; set; }
        [LoadColumn(131)] public float A5LastAwayTeamGoalDiffAft { get; set; }
        [LoadColumn(132)] public float A5LastAwayTeamPosBef { get; set; }
        [LoadColumn(133)] public float A5LastAwayTeamPosAft { get; set; }
        [LoadColumn(134)] public float A5LastAwayTeamScoreBreak { get; set; }
        [LoadColumn(135)] public float A5LastAwayTeamScore { get; set; }
        [LoadColumn(136)] public string A5MatchResult { get; set; }
    }
}
