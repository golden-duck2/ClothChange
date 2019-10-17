using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomName
{
    private static string[] shopNamePartsList = new string[] { "Grazia" ,"Cristina" ,"Amore" ,"Sandra" ,"Pia" ,"Stefanelli" ,"Susanna" ,"Ale" ,"Pavan" ,"Flavia" ,"L." ,"Romagnoli" ,"Ivana" ,"Sofia" ,"Santamaria" ,"Piera" ,"B." ,"Polo" ,"Jennifer" ,"Carmela" ,"De" ,"Lucia" ,"Cristiana" ,"Luna" ,"Pasquini" ,"Titti" ,"Concetta" ,"Licciardello" ,"Lucy" ,"Mihaela" ,"Lucchini" ,"Loretta" ,"Salvatore" ,"Mazzoli" ,"Maria" ,"Concetta" ,"Margherita" ,"Di" ,"Caprio" ,"Rosangela" ,"H." ,"Sciascia" ,"Fiorenza" ,"Lilly" ,"Lambiase" ,"Germana" ,"Angy" ,"Sensi" ,"Natalina" ,"Marianna" ,"Alexandru" ,"Pinuccia" ,"B" ,"Tarquini" ,"Jasmin" ,"E" ,"Lorenzoni" ,"Annunziata" ,"Franco" ,"Aurelio" ,"Mimi" ,"Gina" ,"Ion" ,"Alessandro" ,"Lucrezia" ,"Caporaso" ,"Marie" ,"Fede" ,"Cinquegrana" ,"Naty" ,"Margot" ,"Di" ,"Noto" ,"Molly" ,"Wendy" ,"Mason" ,"Palmina" ,"Samantha" ,"Scalici" ,"Mimoza" ,"Claire" ,"Adriano" ,"Soraya" ,"Milena" ,"Rotunno" ,"Jlenia" ,"Puffa" ,"Calderoni" ,"Dominique" ,"Raluca" ,"Beqiri" ,"Elga" ,"Elisabeth" ,"Giangrande" };

    public static string GetShopName()
    {
        var len = shopNamePartsList.Length;

        return shopNamePartsList [Random.Range(0, len)] + " " + shopNamePartsList[ Random.Range(0, len)];
    }

    private static string[] castNameList = new string[] { "恵弥子", "サトコ", "誌織", "真吏奈", "真帆", "豊香", "結喜", "毬香", "かづえ", "海音", "とし江", "トヨ子", "結紀", "有利恵", "朋弥", "琴", "麻優香", "安沙美", "麻畝", "ゆきほ", "翠子", "なな恵", "りゑ", "未琴", "祥帆", "直絵", "伸世", "みふゆ", "智弓", "嘉世", "愛希", "望都", "夢未", "敬子", "トモミ", "レイカ", "優莉香", "陵", "智理", "亜里子", "朝日香", "亜梨", "花野子", "章恵", "英", "英利", "真奈見", "侑果", "三沙子", "志保乃", "日佳里", "智予", "恵香", "紗慧", "湖乃美", "てる", "涼佳", "さくら", "文美代", "玲依", "美治", "瑛美", "智海", "茉椰", "きき", "黎奈", "曉子", "麻巳", "明貴子", "佐衣", "かつこ", "康代", "世志美", "カナエ", "隆恵", "大好き", "ナツコ", "由那", "侑美子", "花乃", "葵依", "みきえ", "弥子", "五郎", "朱梨", "靖子", "麻沙子", "結美", "風", "多実子", "千子", "純奈", "理生", "美奈美", "熊子", "三惠", "ひよこ", "美秀", "枝利香", "学美", "たず子", "成美", "由乃", "清見", "依奈", "式子", "千永", "夕可里", "彩生", "朱根", "由喜枝", "朝佳", "裕太", "亜葵", "亜莉奈", "之恵", "朗江" };

    public static string GetCastName()
    {
        var len = castNameList.Length;

        return castNameList[Random.Range(0, len)];
    }

    private static string[] castFullName = new string[] { "吉川 直美", "今井 美奈", "荒木 明美", "李 真理", "小川 麻由美", "玉井 彩", "小林 るり子", "青木 彰子", "鳥居 麻衣子", "安達 裕美", "前田 真衣子", "山岡 みどり", "足立 真帆", "岡田 早百合", "村井 志織", "中野 和", "西川 多恵", "北川 千恵美", "村松 若菜", "柴田 明恵", "奥田 亜里沙", "松原 美也子", "高尾 妙子", "大石 佳世子", "田辺 晴子", "渋谷 美沙子", "奈良 あすか", "河野 夕貴", "宮内 りえ", "江川 咲", "吉本 秀子", "矢島 沙希", "浅田 ゆみ", "宮城 実希", "永井 良恵", "野中 美紗子", "奥野 汐里", "岩永 優香", "山野 紗織", "河原 素子", "大田 美香子", "大崎 昌美", "宇野 咲希", "岡野 明香", "中井 純代", "織田 今日子", "須田 光恵", "飯塚 弘恵", "古谷 玲", "尾形 睦子", "三好 美那", "鎌田 まさみ", "相川 菜穂子", "溝口 小夜子", "市原 利佳", "福岡 穂奈美", "小柳 ルミ", "高原 和世", "進藤 きよみ", "北山 智絵" };

    public static string GetCastFullName()
    {
        var len = castFullName.Length;

        return castFullName[Random.Range(0, len)];
    }
}
