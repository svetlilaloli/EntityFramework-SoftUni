﻿//Resharper disable InconsistentNaming, CheckNamespace

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BookShop;
using BookShop.Data;
using BookShop.DataProcessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

[TestFixture]
public class ImportAuthorsTest
{
    private IServiceProvider serviceProvider;

    private static readonly Assembly CurrentAssembly = typeof(StartUp).Assembly;

    [SetUp]
    public void Setup()
    {
        this.serviceProvider = ConfigureServices<BookShopContext>("TeisterMask");
    }

    [Test]
    public void ImportAuthorsZeroTest()
    {
        var context = this.serviceProvider.GetService<BookShopContext>();

        SeedDatabase(context);

        var inputJson =
            "[{\"FirstName\":\"K\",\"LastName\":\"Tribbeck\",\"Phone\":\"808-944-5051\",\"Email\":\"btribbeck0@last.fm\",\"Books\":[{\"Id\":79},{\"Id\":40}]},{\"FirstName\":\"Maridel\",\"LastName\":\"N\",\"Phone\":\"658-437-4751\",\"Email\":\"mdeamaya1@theatlantic.com\",\"Books\":[{\"Id\":117},{\"Id\":88}]},{\"FirstName\":\"Jedd\",\"LastName\":\"Ornils\",\"Phone\":\"81\",\"Email\":\"jornils2@gnu.org\",\"Books\":[{\"Id\":10},{\"Id\":5}]},{\"FirstName\":\"Royal\",\"LastName\":\"Stitcher\",\"Phone\":\"477-740-8767\",\"Email\":\"rstitcher3g.co\",\"Books\":[{\"Id\":73}]},{\"FirstName\":\"Davie\",\"LastName\":\"Maxted\",\"Phone\":\"285-645-6798\",\"Email\":\"dmaxted4@ftc.gov\",\"Books\":[{\"Id\":null}]},{\"FirstName\":\"Nataniel\",\"LastName\":\"Pembery\",\"Phone\":\"343-101-5329\",\"Email\":\"npembery5@shop-pro.jp\",\"Books\":[{\"Id\":104},{\"Id\":96},{\"Id\":24},{\"Id\":34},{\"Id\":null}]},{\"FirstName\":\"Aila\",\"LastName\":\"Fallanche\",\"Phone\":\"142-613-9240\",\"Email\":\"afallanche6@simplemachines.org\",\"Books\":[{\"Id\":23},{\"Id\":null},{\"Id\":101},{\"Id\":69}]},{\"FirstName\":\"Terri\",\"LastName\":\"Credland\",\"Phone\":\"277-480-3447\",\"Email\":\"tcredland7@wp.com\",\"Books\":[{\"Id\":108},{\"Id\":14},{\"Id\":25},{\"Id\":95}]},{\"FirstName\":\"Philbert\",\"LastName\":\"Canland\",\"Phone\":\"118-968-8333\",\"Email\":\"pcanland8@baidu.com\",\"Books\":[{\"Id\":88},{\"Id\":112},{\"Id\":44},{\"Id\":79}]},{\"FirstName\":\"Adora\",\"LastName\":\"Albinson\",\"Phone\":\"580-171-5304\",\"Email\":\"aalbinson9@smh.com.au\",\"Books\":[{\"Id\":83},{\"Id\":102},{\"Id\":59}]},{\"FirstName\":\"Chanda\",\"LastName\":\"Adame\",\"Phone\":\"531-686-6675\",\"Email\":\"cadamea@zimbio.com\",\"Books\":[{\"Id\":118},{\"Id\":92},{\"Id\":117},{\"Id\":69}]},{\"FirstName\":\"Misti\",\"LastName\":\"Dwight\",\"Phone\":\"293-652-2724\",\"Email\":\"mdwightb@usda.gov\",\"Books\":[{\"Id\":11},{\"Id\":null}]},{\"FirstName\":\"Kellen\",\"LastName\":\"Koppke\",\"Phone\":\"154-909-0508\",\"Email\":\"kkoppkec@com.com\",\"Books\":[{\"Id\":65},{\"Id\":22}]},{\"FirstName\":\"Demetre\",\"LastName\":\"Simeons\",\"Phone\":\"795-678-6253\",\"Email\":\"dsimeonsd@istockphoto.com\",\"Books\":[{\"Id\":42},{\"Id\":58},{\"Id\":4}]},{\"FirstName\":null,\"LastName\":\"Emanuelov\",\"Phone\":\"132-254-8909\",\"Email\":null,\"Books\":[{\"Id\":23}]},{\"FirstName\":\"Izaak\",\"LastName\":\"Birkenshaw\",\"Phone\":\"637-464-7998\",\"Email\":\"ibirkenshawf@umich.edu\",\"Books\":[{\"Id\":75},{\"Id\":81},{\"Id\":52},{\"Id\":101}]},{\"FirstName\":\"Sula\",\"LastName\":\"MacTeague\",\"Phone\":\"522-631-8457\",\"Email\":\"smacteagueg@deliciousdays.com\",\"Books\":[{\"Id\":28},{\"Id\":94}]},{\"FirstName\":\"Kalli\",\"LastName\":\"Want\",\"Phone\":\"951-897-3479\",\"Email\":\"kwanth@infoseek.co.jp\",\"Books\":[{\"Id\":114},{\"Id\":null}]},{\"FirstName\":\"Jaclin\",\"LastName\":\"Sharvill\",\"Phone\":\"951-822-5488\",\"Email\":\"jsharvilli@wikispaces.com\",\"Books\":[{\"Id\":104}]},{\"FirstName\":\"Fleming\",\"LastName\":null,\"Phone\":\"767-318-2767\",\"Email\":\"frandlesonj@hc360.com\",\"Books\":[{\"Id\":null},{\"Id\":4}]},{\"FirstName\":\"Sax\",\"LastName\":\"Careswell\",\"Phone\":\"387-691-8108\",\"Email\":\"scareswellk@xinhuanet.com\",\"Books\":[{\"Id\":100},{\"Id\":14}]},{\"FirstName\":\"Fianna\",\"LastName\":\"Dripps\",\"Phone\":\"765-845-6325\",\"Email\":\"fdrippsl@reverbnation.com\",\"Books\":[{\"Id\":43},{\"Id\":46},{\"Id\":9}]},{\"FirstName\":\"Hettie\",\"LastName\":\"Cattenach\",\"Phone\":\"588-104-9281\",\"Email\":\"hcattenachm@ovh.net\",\"Books\":[{\"Id\":20},{\"Id\":102},{\"Id\":7},{\"Id\":104},{\"Id\":103}]},{\"FirstName\":\"Austin\",\"LastName\":\"Hollingby\",\"Phone\":\"847-135-7950\",\"Email\":\"ahollingbyn@diigo.com\",\"Books\":[{\"Id\":26}]},{\"FirstName\":\"Sybyl\",\"LastName\":\"Gasking\",\"Phone\":\"173-535-3632\",\"Email\":\"sgaskingo@ca.gov\",\"Books\":[{\"Id\":111},{\"Id\":86},{\"Id\":4},{\"Id\":87},{\"Id\":33}]},{\"FirstName\":\"Susy\",\"LastName\":\"Breinl\",\"Phone\":\"985-611-5342\",\"Email\":\"sbreinlp@google.es\",\"Books\":[{\"Id\":69},{\"Id\":67},{\"Id\":82},{\"Id\":114}]},{\"FirstName\":\"Eldin\",\"LastName\":\"Stowell\",\"Phone\":\"671-858-6112\",\"Email\":\"estowellq@joomla.org\",\"Books\":[{\"Id\":111},{\"Id\":34},{\"Id\":8},{\"Id\":43}]},{\"FirstName\":\"Giovanni\",\"LastName\":\"Marcham\",\"Phone\":\"334-983-8105\",\"Email\":\"gmarchamr@uiuc.edu\",\"Books\":[{\"Id\":54}]},{\"FirstName\":\"Porty\",\"LastName\":\"Snookes\",\"Phone\":\"757-479-2113\",\"Email\":\"psnookess@independent.co.uk\",\"Books\":[{\"Id\":99},{\"Id\":6},{\"Id\":21}]},{\"FirstName\":\"Gerrie\",\"LastName\":\"Leatham\",\"Phone\":\"904-474-1608\",\"Email\":\"gleathamt@skyrock.com\",\"Books\":[{\"Id\":49},{\"Id\":null},{\"Id\":21},{\"Id\":83}]},{\"FirstName\":\"Ashleigh\",\"LastName\":\"Beggi\",\"Phone\":\"486-407-7149\",\"Email\":\"abeggiu@cafepress.com\",\"Books\":[{\"Id\":63},{\"Id\":33},{\"Id\":75},{\"Id\":null},{\"Id\":49}]},{\"FirstName\":\"Bradford\",\"LastName\":\"Larderot\",\"Phone\":\"934-360-9692\",\"Email\":\"blarderotv@dropbox.com\",\"Books\":[{\"Id\":34},{\"Id\":49},{\"Id\":13},{\"Id\":51}]},{\"FirstName\":\"Cornall\",\"LastName\":\"Morgon\",\"Phone\":\"556-825-2071\",\"Email\":\"cmorgonw@scientificamerican.com\",\"Books\":[{\"Id\":40}]},{\"FirstName\":\"Gris\",\"LastName\":\"Rozea\",\"Phone\":\"875-651-0634\",\"Email\":\"grozeax@facebook.com\",\"Books\":[{\"Id\":83}]},{\"FirstName\":\"Neron\",\"LastName\":\"Winterflood\",\"Phone\":\"855-769-8818\",\"Email\":\"nwinterfloody@tripod.com\",\"Books\":[{\"Id\":19},{\"Id\":90},{\"Id\":5},{\"Id\":null},{\"Id\":79}]},{\"FirstName\":\"Gretel\",\"LastName\":\"Mersh\",\"Phone\":\"126-272-1610\",\"Email\":\"gmershz@irs.gov\",\"Books\":[{\"Id\":22},{\"Id\":85},{\"Id\":45},{\"Id\":40}]},{\"FirstName\":\"Meredeth\",\"LastName\":\"MacCostye\",\"Phone\":\"210-463-5830\",\"Email\":\"mmaccostye10@moonfruit.com\",\"Books\":[{\"Id\":108},{\"Id\":9},{\"Id\":56}]},{\"FirstName\":\"Quinn\",\"LastName\":\"Gadie\",\"Phone\":\"995-546-6978\",\"Email\":\"qgadie11@altervista.org\",\"Books\":[{\"Id\":84},{\"Id\":null}]},{\"FirstName\":\"Lloyd\",\"LastName\":\"Jickells\",\"Phone\":\"987-139-5281\",\"Email\":\"ljickells12@springer.com\",\"Books\":[{\"Id\":47},{\"Id\":96}]},{\"FirstName\":\"Melody\",\"LastName\":\"Binyon\",\"Phone\":\"160-690-1149\",\"Email\":\"mbinyon13@earthlink.net\",\"Books\":[{\"Id\":44},{\"Id\":17},{\"Id\":115},{\"Id\":85},{\"Id\":101}]},{\"FirstName\":\"Tally\",\"LastName\":\"Salzen\",\"Phone\":\"737-565-4507\",\"Email\":\"tsalzen14@umich.edu\",\"Books\":[{\"Id\":91},{\"Id\":116},{\"Id\":30},{\"Id\":null}]},{\"FirstName\":\"Carr\",\"LastName\":\"Westney\",\"Phone\":\"108-700-4443\",\"Email\":\"cwestney15@qq.com\",\"Books\":[{\"Id\":70}]},{\"FirstName\":\"Angelina\",\"LastName\":\"Tallet\",\"Phone\":\"695-282-9720\",\"Email\":\"atallet16@squarespace.com\",\"Books\":[{\"Id\":67},{\"Id\":93},{\"Id\":64},{\"Id\":31},{\"Id\":21}]},{\"FirstName\":\"Sharline\",\"LastName\":\"Lobley\",\"Phone\":\"423-844-8912\",\"Email\":\"slobley17@nhs.uk\",\"Books\":[{\"Id\":111},{\"Id\":79}]},{\"FirstName\":\"Charo\",\"LastName\":\"Blazey\",\"Phone\":\"916-101-4919\",\"Email\":\"cblazey18@technorati.com\",\"Books\":[{\"Id\":53},{\"Id\":44}]},{\"FirstName\":\"Holly-anne\",\"LastName\":\"Nannini\",\"Phone\":\"756-854-5456\",\"Email\":\"hnannini19@squidoo.com\",\"Books\":[{\"Id\":57},{\"Id\":83},{\"Id\":64},{\"Id\":77},{\"Id\":69}]},{\"FirstName\":\"Lorelle\",\"LastName\":\"Ingledew\",\"Phone\":\"939-651-9389\",\"Email\":\"lingledew1a@amazon.co.uk\",\"Books\":[{\"Id\":96},{\"Id\":70}]},{\"FirstName\":\"Aubert\",\"LastName\":\"Vignal\",\"Phone\":\"305-777-7813\",\"Email\":\"avignal1b@nbcnews.com\",\"Books\":[{\"Id\":26},{\"Id\":100},{\"Id\":50}]},{\"FirstName\":\"Caroljean\",\"LastName\":\"Bisgrove\",\"Phone\":\"472-703-3006\",\"Email\":\"cbisgrove1c@gizmodo.com\",\"Books\":[{\"Id\":83},{\"Id\":38}]},{\"FirstName\":\"Charlotte\",\"LastName\":\"Scamadin\",\"Phone\":\"786-924-5038\",\"Email\":\"cscamadin1d@dailymotion.com\",\"Books\":[{\"Id\":33},{\"Id\":97},{\"Id\":29},{\"Id\":null},{\"Id\":113}]},{\"FirstName\":\"Kristin\",\"LastName\":\"Roddick\",\"Phone\":\"114-900-1773\",\"Email\":\"kroddick1e@house.gov\",\"Books\":[{\"Id\":69},{\"Id\":61},{\"Id\":92}]},{\"FirstName\":\"Torrey\",\"LastName\":\"Ricardon\",\"Phone\":\"753-107-1314\",\"Email\":\"tricardon1f@flavors.me\",\"Books\":[{\"Id\":81},{\"Id\":44},{\"Id\":113},{\"Id\":30},{\"Id\":45}]},{\"FirstName\":\"Ninetta\",\"LastName\":\"Omar\",\"Phone\":\"587-747-1935\",\"Email\":\"nomar1g@yale.edu\",\"Books\":[{\"Id\":112}]},{\"FirstName\":\"Alden\",\"LastName\":\"Vellden\",\"Phone\":\"280-125-7176\",\"Email\":\"avellden1h@umich.edu\",\"Books\":[{\"Id\":60},{\"Id\":null},{\"Id\":83}]},{\"FirstName\":\"Odella\",\"LastName\":\"Shurrock\",\"Phone\":\"322-611-4364\",\"Email\":\"oshurrock1i@imgur.com\",\"Books\":[{\"Id\":30},{\"Id\":114},{\"Id\":51}]},{\"FirstName\":\"Ketty\",\"LastName\":\"Comerford\",\"Phone\":\"521-256-8694\",\"Email\":\"kcomerford1j@moonfruit.com\",\"Books\":[{\"Id\":6},{\"Id\":3},{\"Id\":115}]},{\"FirstName\":null,\"LastName\":\"Riccioppo\",\"Phone\":\"764-459-3022\",\"Email\":null,\"Books\":[{\"Id\":90}]},{\"FirstName\":\"Wittie\",\"LastName\":\"Pedlow\",\"Phone\":\"836-531-3380\",\"Email\":\"wpedlow1l@flickr.com\",\"Books\":[{\"Id\":99},{\"Id\":28},{\"Id\":52},{\"Id\":42},{\"Id\":95}]},{\"FirstName\":\"Orin\",\"LastName\":\"Jakubovski\",\"Phone\":\"593-609-4208\",\"Email\":\"ojakubovski1m@livejournal.com\",\"Books\":[{\"Id\":6},{\"Id\":65},{\"Id\":108},{\"Id\":18},{\"Id\":42}]},{\"FirstName\":\"Whitaker\",\"LastName\":\"Hursthouse\",\"Phone\":\"433-972-7281\",\"Email\":\"whursthouse1n@google.ru\",\"Books\":[{\"Id\":103},{\"Id\":41},{\"Id\":24},{\"Id\":69},{\"Id\":112}]},{\"FirstName\":\"Oriana\",\"LastName\":\"Brounfield\",\"Phone\":\"109-537-6331\",\"Email\":\"obrounfield1o@vimeo.com\",\"Books\":[{\"Id\":87},{\"Id\":17},{\"Id\":55},{\"Id\":107},{\"Id\":33}]},{\"FirstName\":null,\"LastName\":\"Bramwich\",\"Phone\":\"540-712-4198\",\"Email\":null,\"Books\":[{\"Id\":115}]},{\"FirstName\":\"Isahella\",\"LastName\":\"Zelland\",\"Phone\":\"508-681-1324\",\"Email\":\"izelland1q@unc.edu\",\"Books\":[{\"Id\":110},{\"Id\":39},{\"Id\":108}]},{\"FirstName\":\"Marji\",\"LastName\":\"Burman\",\"Phone\":\"572-388-8681\",\"Email\":\"mburman1r@gravatar.com\",\"Books\":[{\"Id\":93}]},{\"FirstName\":\"Karla\",\"LastName\":\"Kinnear\",\"Phone\":\"461-361-0505\",\"Email\":\"kkinnear1s@tripadvisor.com\",\"Books\":[{\"Id\":84},{\"Id\":15},{\"Id\":88}]},{\"FirstName\":\"Auroora\",\"LastName\":\"O' Dooley\",\"Phone\":\"255-440-0223\",\"Email\":\"aodooley1t@wsj.com\",\"Books\":[{\"Id\":83},{\"Id\":52}]},{\"FirstName\":\"Hendrick\",\"LastName\":\"Cleminson\",\"Phone\":\"900-502-3568\",\"Email\":\"hcleminson1u@topsy.com\",\"Books\":[{\"Id\":120},{\"Id\":64},{\"Id\":30}]},{\"FirstName\":\"Tab\",\"LastName\":\"Cockman\",\"Phone\":\"606-706-4274\",\"Email\":\"tcockman1v@shutterfly.com\",\"Books\":[{\"Id\":81},{\"Id\":56},{\"Id\":62},{\"Id\":4}]},{\"FirstName\":\"Jermaine\",\"LastName\":\"Alpine\",\"Phone\":\"969-639-3796\",\"Email\":\"jalpine1w@weibo.com\",\"Books\":[{\"Id\":46},{\"Id\":116},{\"Id\":120}]},{\"FirstName\":\"Trumann\",\"LastName\":\"Charer\",\"Phone\":\"874-718-8280\",\"Email\":\"tcharer1x@360.cn\",\"Books\":[{\"Id\":null},{\"Id\":109}]},{\"FirstName\":\"Pearce\",\"LastName\":\"Gibbon\",\"Phone\":\"198-688-2310\",\"Email\":\"pgibbon1y@nytimes.com\",\"Books\":[{\"Id\":57},{\"Id\":77}]},{\"FirstName\":\"Dalton\",\"LastName\":\"Sinclaire\",\"Phone\":\"913-492-2253\",\"Email\":\"dsinclaire1z@irs.gov\",\"Books\":[{\"Id\":7},{\"Id\":97},{\"Id\":99}]},{\"FirstName\":\"Tommy\",\"LastName\":\"Click\",\"Phone\":\"687-754-1415\",\"Email\":\"tclick20@omniture.com\",\"Books\":[{\"Id\":61},{\"Id\":30},{\"Id\":40},{\"Id\":41}]},{\"FirstName\":\"Gerald\",\"LastName\":\"Laxe\",\"Phone\":\"171-411-2903\",\"Email\":\"glaxe21@marriott.com\",\"Books\":[{\"Id\":96}]},{\"FirstName\":\"Abdel\",\"LastName\":\"Ponnsett\",\"Phone\":\"919-455-7061\",\"Email\":\"aponnsett22@yolasite.com\",\"Books\":[{\"Id\":43},{\"Id\":106},{\"Id\":28}]},{\"FirstName\":\"Gayla\",\"LastName\":\"Janoch\",\"Phone\":\"337-247-8992\",\"Email\":\"gjanoch23@nps.gov\",\"Books\":[{\"Id\":34},{\"Id\":54},{\"Id\":110}]},{\"FirstName\":\"Carmela\",\"LastName\":\"Clemerson\",\"Phone\":\"435-368-5249\",\"Email\":\"cclemerson24@gizmodo.com\",\"Books\":[{\"Id\":10},{\"Id\":24},{\"Id\":4}]},{\"FirstName\":\"Noella\",\"LastName\":\"Coslitt\",\"Phone\":\"785-933-3006\",\"Email\":\"ncoslitt25@prweb.com\",\"Books\":[{\"Id\":62},{\"Id\":50},{\"Id\":null},{\"Id\":51}]},{\"FirstName\":\"Ashia\",\"LastName\":\"Esh\",\"Phone\":\"671-170-9184\",\"Email\":\"aesh26@sitemeter.com\",\"Books\":[{\"Id\":106},{\"Id\":64},{\"Id\":57},{\"Id\":21},{\"Id\":32}]},{\"FirstName\":\"Marsiella\",\"LastName\":\"Fulun\",\"Phone\":\"608-762-4403\",\"Email\":\"mfulun27@icio.us\",\"Books\":[{\"Id\":58}]},{\"FirstName\":\"Jenny\",\"LastName\":\"Boote\",\"Phone\":\"491-779-5848\",\"Email\":\"jboote28@multiply.com\",\"Books\":[{\"Id\":108},{\"Id\":105},{\"Id\":55}]},{\"FirstName\":\"Valene\",\"LastName\":\"Billham\",\"Phone\":\"568-198-5531\",\"Email\":\"vbillham29@themeforest.net\",\"Books\":[{\"Id\":106},{\"Id\":16},{\"Id\":80}]},{\"FirstName\":\"Adelaida\",\"LastName\":\"Begwell\",\"Phone\":\"892-210-0399\",\"Email\":\"abegwell2a@yellowpages.com\",\"Books\":[{\"Id\":54},{\"Id\":109},{\"Id\":42},{\"Id\":null},{\"Id\":24}]},{\"FirstName\":\"Stanly\",\"LastName\":\"Starcks\",\"Phone\":\"915-600-0699\",\"Email\":\"sstarcks2b@spotify.com\",\"Books\":[{\"Id\":33},{\"Id\":7}]},{\"FirstName\":\"Veronique\",\"LastName\":\"Legges\",\"Phone\":\"195-654-7566\",\"Email\":\"vlegges2c@wp.com\",\"Books\":[{\"Id\":92},{\"Id\":28},{\"Id\":16}]},{\"FirstName\":\"Georgine\",\"LastName\":\"Moscon\",\"Phone\":\"771-527-2167\",\"Email\":\"gmoscon2d@wikia.com\",\"Books\":[{\"Id\":113}]},{\"FirstName\":\"Regan\",\"LastName\":\"Jertz\",\"Phone\":\"131-273-6926\",\"Email\":\"rjertz2e@unc.edu\",\"Books\":[{\"Id\":9},{\"Id\":85}]},{\"FirstName\":\"Cynthy\",\"LastName\":\"Boichat\",\"Phone\":\"882-509-2981\",\"Email\":\"cboichat2f@ox.ac.uk\",\"Books\":[{\"Id\":23},{\"Id\":24},{\"Id\":13},{\"Id\":8},{\"Id\":98}]},{\"FirstName\":\"Janelle\",\"LastName\":\"Seppey\",\"Phone\":\"101-659-2330\",\"Email\":\"jseppey2g@feedburner.com\",\"Books\":[{\"Id\":21},{\"Id\":108},{\"Id\":27}]},{\"FirstName\":\"Nobie\",\"LastName\":\"Buddington\",\"Phone\":\"547-457-9086\",\"Email\":\"nbuddington2h@samsung.com\",\"Books\":[{\"Id\":23},{\"Id\":103},{\"Id\":39}]},{\"FirstName\":\"Jaye\",\"LastName\":\"Fishleigh\",\"Phone\":\"677-803-2318\",\"Email\":\"jfishleigh2i@state.tx.us\",\"Books\":[{\"Id\":75}]},{\"FirstName\":null,\"LastName\":\"Wilstead\",\"Phone\":\"737-832-0307\",\"Email\":null,\"Books\":[{\"Id\":105},{\"Id\":100},{\"Id\":45},{\"Id\":null}]},{\"FirstName\":\"Cirstoforo\",\"LastName\":\"McCooke\",\"Phone\":\"141-541-3583\",\"Email\":\"cmccooke2k@naver.com\",\"Books\":[{\"Id\":119},{\"Id\":null}]},{\"FirstName\":\"Hall\",\"LastName\":\"Frear\",\"Phone\":\"132-964-3314\",\"Email\":\"hfrear2l@netlog.com\",\"Books\":[{\"Id\":51},{\"Id\":72},{\"Id\":89},{\"Id\":117},{\"Id\":67}]},{\"FirstName\":null,\"LastName\":\"Bleackly\",\"Phone\":\"855-867-3211\",\"Email\":null,\"Books\":[{\"Id\":58},{\"Id\":12},{\"Id\":null}]},{\"FirstName\":\"Joycelin\",\"LastName\":null,\"Phone\":\"390-270-3797\",\"Email\":\"jcrossland2n@go.com\",\"Books\":[{\"Id\":null},{\"Id\":45},{\"Id\":113},{\"Id\":102},{\"Id\":2}]},{\"FirstName\":\"Erena\",\"LastName\":\"Harrow\",\"Phone\":\"274-661-9412\",\"Email\":\"eharrow2o@shutterfly.com\",\"Books\":[{\"Id\":89}]},{\"FirstName\":\"Miles\",\"LastName\":\"Flockhart\",\"Phone\":\"342-392-8973\",\"Email\":\"mflockhart2p@indiatimes.com\",\"Books\":[{\"Id\":115},{\"Id\":8},{\"Id\":42},{\"Id\":50}]},{\"FirstName\":\"Dietrich\",\"LastName\":\"Granville\",\"Phone\":\"756-179-0948\",\"Email\":\"dgranville2q@mlb.com\",\"Books\":[{\"Id\":29},{\"Id\":99}]},{\"FirstName\":\"Giustino\",\"LastName\":\"Tinkler\",\"Phone\":\"736-844-6719\",\"Email\":\"gtinkler2r@jimdo.com\",\"Books\":[{\"Id\":118},{\"Id\":1},{\"Id\":7}]}]";

        var actualOutput =
            Deserializer.ImportAuthors(context, inputJson).TrimEnd();

        var expectedOutput =
            "Invalid data!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nSuccessfully imported author - Nataniel Pembery with 2 books.\r\nSuccessfully imported author - Aila Fallanche with 2 books.\r\nSuccessfully imported author - Terri Credland with 2 books.\r\nSuccessfully imported author - Philbert Canland with 1 books.\r\nSuccessfully imported author - Adora Albinson with 1 books.\r\nSuccessfully imported author - Chanda Adame with 1 books.\r\nSuccessfully imported author - Misti Dwight with 1 books.\r\nSuccessfully imported author - Kellen Koppke with 2 books.\r\nSuccessfully imported author - Demetre Simeons with 3 books.\r\nInvalid data!\r\nSuccessfully imported author - Izaak Birkenshaw with 1 books.\r\nSuccessfully imported author - Sula MacTeague with 1 books.\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nSuccessfully imported author - Sax Careswell with 1 books.\r\nSuccessfully imported author - Fianna Dripps with 3 books.\r\nSuccessfully imported author - Hettie Cattenach with 2 books.\r\nSuccessfully imported author - Austin Hollingby with 1 books.\r\nSuccessfully imported author - Sybyl Gasking with 2 books.\r\nSuccessfully imported author - Susy Breinl with 2 books.\r\nSuccessfully imported author - Eldin Stowell with 3 books.\r\nSuccessfully imported author - Giovanni Marcham with 1 books.\r\nSuccessfully imported author - Porty Snookes with 2 books.\r\nSuccessfully imported author - Gerrie Leatham with 2 books.\r\nSuccessfully imported author - Ashleigh Beggi with 3 books.\r\nSuccessfully imported author - Bradford Larderot with 4 books.\r\nSuccessfully imported author - Cornall Morgon with 1 books.\r\nInvalid data!\r\nSuccessfully imported author - Neron Winterflood with 2 books.\r\nSuccessfully imported author - Gretel Mersh with 3 books.\r\nSuccessfully imported author - Meredeth MacCostye with 2 books.\r\nInvalid data!\r\nSuccessfully imported author - Lloyd Jickells with 1 books.\r\nSuccessfully imported author - Melody Binyon with 2 books.\r\nSuccessfully imported author - Tally Salzen with 1 books.\r\nSuccessfully imported author - Carr Westney with 1 books.\r\nSuccessfully imported author - Angelina Tallet with 4 books.\r\nInvalid data!\r\nSuccessfully imported author - Charo Blazey with 2 books.\r\nSuccessfully imported author - Holly-anne Nannini with 3 books.\r\nSuccessfully imported author - Lorelle Ingledew with 1 books.\r\nSuccessfully imported author - Aubert Vignal with 2 books.\r\nSuccessfully imported author - Caroljean Bisgrove with 1 books.\r\nSuccessfully imported author - Charlotte Scamadin with 2 books.\r\nSuccessfully imported author - Kristin Roddick with 2 books.\r\nSuccessfully imported author - Torrey Ricardon with 3 books.\r\nInvalid data!\r\nSuccessfully imported author - Alden Vellden with 1 books.\r\nSuccessfully imported author - Odella Shurrock with 2 books.\r\nSuccessfully imported author - Ketty Comerford with 2 books.\r\nInvalid data!\r\nSuccessfully imported author - Wittie Pedlow with 3 books.\r\nSuccessfully imported author - Orin Jakubovski with 4 books.\r\nSuccessfully imported author - Whitaker Hursthouse with 3 books.\r\nSuccessfully imported author - Oriana Brounfield with 3 books.\r\nInvalid data!\r\nSuccessfully imported author - Isahella Zelland with 1 books.\r\nInvalid data!\r\nSuccessfully imported author - Karla Kinnear with 1 books.\r\nSuccessfully imported author - Auroora O' Dooley with 1 books.\r\nSuccessfully imported author - Hendrick Cleminson with 2 books.\r\nSuccessfully imported author - Tab Cockman with 3 books.\r\nSuccessfully imported author - Jermaine Alpine with 1 books.\r\nInvalid data!\r\nSuccessfully imported author - Pearce Gibbon with 1 books.\r\nSuccessfully imported author - Dalton Sinclaire with 1 books.\r\nSuccessfully imported author - Tommy Click with 4 books.\r\nInvalid data!\r\nSuccessfully imported author - Abdel Ponnsett with 2 books.\r\nSuccessfully imported author - Gayla Janoch with 2 books.\r\nSuccessfully imported author - Carmela Clemerson with 3 books.\r\nSuccessfully imported author - Noella Coslitt with 3 books.\r\nSuccessfully imported author - Ashia Esh with 4 books.\r\nSuccessfully imported author - Marsiella Fulun with 1 books.\r\nSuccessfully imported author - Jenny Boote with 1 books.\r\nSuccessfully imported author - Valene Billham with 1 books.\r\nSuccessfully imported author - Adelaida Begwell with 3 books.\r\nSuccessfully imported author - Stanly Starcks with 2 books.\r\nSuccessfully imported author - Veronique Legges with 2 books.\r\nInvalid data!\r\nSuccessfully imported author - Regan Jertz with 1 books.\r\nSuccessfully imported author - Cynthy Boichat with 4 books.\r\nSuccessfully imported author - Janelle Seppey with 2 books.\r\nSuccessfully imported author - Nobie Buddington with 2 books.\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nSuccessfully imported author - Hall Frear with 2 books.\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nSuccessfully imported author - Miles Flockhart with 3 books.\r\nSuccessfully imported author - Dietrich Granville with 1 books.\r\nSuccessfully imported author - Giustino Tinkler with 2 books.";

        var assertContext = this.serviceProvider.GetService<BookShopContext>();

        const int expectedAuthorsCount = 75;
        var actualAuthorsCount = assertContext.Authors.Count();

        const int expectedAuthorsBooksCount = 150;
        var actualAuthorsBooksCount = assertContext.AuthorsBooks.Count();

        Assert.That(actualAuthorsBooksCount, Is.EqualTo(expectedAuthorsBooksCount),
            $"Inserted {nameof(context.AuthorsBooks)} count is incorrect!");

        Assert.That(actualAuthorsCount, Is.EqualTo(expectedAuthorsCount),
            $"Inserted {nameof(context.Authors)} count is incorrect!");

        Assert.That(actualOutput, Is.EqualTo(expectedOutput).NoClip,
            $"{nameof(Deserializer.ImportAuthors)} output is incorrect!");
    }

    private static void SeedDatabase(BookShopContext context)
    {
        var datasetsJson =
            "{\"Book\":[{\"Id\":1,\"Name\":\"Hairy Torchwood\",\"Genre\":3,\"Price\":41.99,\"Pages\":3013,\"PublishedOn\":\"2013-01-13T00:00:00\"},{\"Id\":2,\"Name\":\"Hayfield Tarweed\",\"Genre\":3,\"Price\":51.69,\"Pages\":3722,\"PublishedOn\":\"2017-10-24T00:00:00\"},{\"Id\":3,\"Name\":\"Pinon\",\"Genre\":1,\"Price\":14.1,\"Pages\":2258,\"PublishedOn\":\"2016-06-20T00:00:00\"},{\"Id\":4,\"Name\":\"Wideleaf Schistidium Moss\",\"Genre\":2,\"Price\":16.26,\"Pages\":1241,\"PublishedOn\":\"2015-11-05T00:00:00\"},{\"Id\":5,\"Name\":\"Featherstem Clubmoss\",\"Genre\":2,\"Price\":89.77,\"Pages\":3043,\"PublishedOn\":\"2014-08-01T00:00:00\"},{\"Id\":6,\"Name\":\"Tapertip Rush\",\"Genre\":1,\"Price\":49.34,\"Pages\":131,\"PublishedOn\":\"2015-05-28T00:00:00\"},{\"Id\":7,\"Name\":\"Australian Desert Lime\",\"Genre\":1,\"Price\":42.29,\"Pages\":1719,\"PublishedOn\":\"2019-05-07T00:00:00\"},{\"Id\":8,\"Name\":\"Silverweed\",\"Genre\":2,\"Price\":88.66,\"Pages\":4328,\"PublishedOn\":\"2012-12-20T00:00:00\"},{\"Id\":9,\"Name\":\"Maui Woodfern\",\"Genre\":2,\"Price\":78.42,\"Pages\":218,\"PublishedOn\":\"2015-05-10T00:00:00\"},{\"Id\":10,\"Name\":\"Roundheaded Leek\",\"Genre\":2,\"Price\":10.41,\"Pages\":2296,\"PublishedOn\":\"2017-01-17T00:00:00\"},{\"Id\":11,\"Name\":\"Pyramid Bugle\",\"Genre\":2,\"Price\":56.74,\"Pages\":2286,\"PublishedOn\":\"2013-10-31T00:00:00\"},{\"Id\":12,\"Name\":\"Spring Thistle\",\"Genre\":3,\"Price\":29.62,\"Pages\":4161,\"PublishedOn\":\"2019-11-09T00:00:00\"},{\"Id\":13,\"Name\":\"Seepweed\",\"Genre\":3,\"Price\":54.05,\"Pages\":2031,\"PublishedOn\":\"2019-01-31T00:00:00\"},{\"Id\":14,\"Name\":\"Singlestem Leather-root\",\"Genre\":2,\"Price\":25.11,\"Pages\":407,\"PublishedOn\":\"2018-09-12T00:00:00\"},{\"Id\":15,\"Name\":\"Gray Dogwood\",\"Genre\":3,\"Price\":17.25,\"Pages\":429,\"PublishedOn\":\"2014-04-09T00:00:00\"},{\"Id\":16,\"Name\":\"Shagbark Hickory\",\"Genre\":1,\"Price\":64.49,\"Pages\":308,\"PublishedOn\":\"2019-11-13T00:00:00\"},{\"Id\":17,\"Name\":\"Ochna\",\"Genre\":2,\"Price\":58.87,\"Pages\":1731,\"PublishedOn\":\"2015-04-02T00:00:00\"},{\"Id\":18,\"Name\":\"Twayblade\",\"Genre\":3,\"Price\":78.31,\"Pages\":620,\"PublishedOn\":\"2019-06-18T00:00:00\"},{\"Id\":19,\"Name\":\"Ocellularia Lichen\",\"Genre\":3,\"Price\":80.61,\"Pages\":1599,\"PublishedOn\":\"2018-01-15T00:00:00\"},{\"Id\":20,\"Name\":\"Sierra Marsh Fern\",\"Genre\":3,\"Price\":42.55,\"Pages\":4881,\"PublishedOn\":\"2016-03-18T00:00:00\"},{\"Id\":21,\"Name\":\"Sky Mousetail\",\"Genre\":1,\"Price\":13.14,\"Pages\":2877,\"PublishedOn\":\"2019-07-29T00:00:00\"},{\"Id\":22,\"Name\":\"Wisconsin Rim Lichen\",\"Genre\":2,\"Price\":57.79,\"Pages\":2414,\"PublishedOn\":\"2017-08-08T00:00:00\"},{\"Id\":23,\"Name\":\"Sphinctrina Lichen\",\"Genre\":1,\"Price\":67.15,\"Pages\":4795,\"PublishedOn\":\"2018-12-27T00:00:00\"},{\"Id\":24,\"Name\":\"Rough Maidenhair\",\"Genre\":2,\"Price\":52.63,\"Pages\":2791,\"PublishedOn\":\"2017-09-03T00:00:00\"},{\"Id\":25,\"Name\":\"Bigelow's Monkeyflower\",\"Genre\":3,\"Price\":19.34,\"Pages\":1870,\"PublishedOn\":\"2015-11-20T00:00:00\"},{\"Id\":26,\"Name\":\"Stebbins' Desertparsley\",\"Genre\":1,\"Price\":34.18,\"Pages\":3443,\"PublishedOn\":\"2014-11-29T00:00:00\"},{\"Id\":27,\"Name\":\"Psoralea Globemallow\",\"Genre\":3,\"Price\":51.18,\"Pages\":165,\"PublishedOn\":\"2013-03-30T00:00:00\"},{\"Id\":28,\"Name\":\"Arizona Century Plant\",\"Genre\":1,\"Price\":43.53,\"Pages\":1771,\"PublishedOn\":\"2017-01-05T00:00:00\"},{\"Id\":29,\"Name\":\"Jamaican Broom\",\"Genre\":1,\"Price\":50.72,\"Pages\":274,\"PublishedOn\":\"2014-08-28T00:00:00\"},{\"Id\":30,\"Name\":\"Borrego Milkvetch\",\"Genre\":2,\"Price\":60.41,\"Pages\":1281,\"PublishedOn\":\"2017-08-15T00:00:00\"},{\"Id\":31,\"Name\":\"Earlyleaf Brome\",\"Genre\":3,\"Price\":63.66,\"Pages\":1379,\"PublishedOn\":\"2019-01-09T00:00:00\"},{\"Id\":32,\"Name\":\"Twoflower Melicgrass\",\"Genre\":2,\"Price\":29.06,\"Pages\":2855,\"PublishedOn\":\"2014-01-09T00:00:00\"},{\"Id\":33,\"Name\":\"Rayless Alkali Aster\",\"Genre\":2,\"Price\":98.99,\"Pages\":137,\"PublishedOn\":\"2013-07-31T00:00:00\"},{\"Id\":34,\"Name\":\"Small Floating Mannagrass\",\"Genre\":1,\"Price\":49.15,\"Pages\":3569,\"PublishedOn\":\"2013-05-16T00:00:00\"},{\"Id\":35,\"Name\":\"Texas Lady's Tresses\",\"Genre\":3,\"Price\":16.34,\"Pages\":4170,\"PublishedOn\":\"2017-12-08T00:00:00\"},{\"Id\":36,\"Name\":\"Winter Vetch\",\"Genre\":2,\"Price\":19.51,\"Pages\":2693,\"PublishedOn\":\"2013-11-13T00:00:00\"},{\"Id\":37,\"Name\":\"Desert Combleaf\",\"Genre\":3,\"Price\":98.72,\"Pages\":2264,\"PublishedOn\":\"2018-03-29T00:00:00\"},{\"Id\":38,\"Name\":\"Candy Barrelcactus\",\"Genre\":1,\"Price\":51.78,\"Pages\":2040,\"PublishedOn\":\"2014-05-03T00:00:00\"},{\"Id\":39,\"Name\":\"Lindley's Clarkia\",\"Genre\":2,\"Price\":0.87,\"Pages\":2293,\"PublishedOn\":\"2015-12-03T00:00:00\"},{\"Id\":40,\"Name\":\"Lapland Poppy\",\"Genre\":1,\"Price\":14.54,\"Pages\":198,\"PublishedOn\":\"2016-04-02T00:00:00\"},{\"Id\":41,\"Name\":\"Macoun's Woodroot\",\"Genre\":3,\"Price\":17.91,\"Pages\":224,\"PublishedOn\":\"2018-07-28T00:00:00\"},{\"Id\":42,\"Name\":\"Airplant\",\"Genre\":3,\"Price\":30.47,\"Pages\":3245,\"PublishedOn\":\"2016-11-24T00:00:00\"},{\"Id\":43,\"Name\":\"Black Bogrush\",\"Genre\":1,\"Price\":3.48,\"Pages\":3620,\"PublishedOn\":\"2013-11-18T00:00:00\"},{\"Id\":44,\"Name\":\"Great Basin Woollystar\",\"Genre\":2,\"Price\":31.74,\"Pages\":415,\"PublishedOn\":\"2014-02-10T00:00:00\"},{\"Id\":45,\"Name\":\"Woodland Spurge\",\"Genre\":1,\"Price\":80.52,\"Pages\":584,\"PublishedOn\":\"2014-05-09T00:00:00\"},{\"Id\":46,\"Name\":\"Gigantochloa\",\"Genre\":3,\"Price\":65.81,\"Pages\":802,\"PublishedOn\":\"2014-08-31T00:00:00\"},{\"Id\":47,\"Name\":\"Drummond's Clematis\",\"Genre\":1,\"Price\":43.43,\"Pages\":1698,\"PublishedOn\":\"2015-01-26T00:00:00\"},{\"Id\":48,\"Name\":\"Shingle Oak\",\"Genre\":2,\"Price\":17.85,\"Pages\":1816,\"PublishedOn\":\"2016-04-29T00:00:00\"},{\"Id\":49,\"Name\":\"Woollyleaf Bur Ragweed\",\"Genre\":3,\"Price\":3.9,\"Pages\":1641,\"PublishedOn\":\"2018-01-24T00:00:00\"},{\"Id\":50,\"Name\":\"Mojave Linanthus\",\"Genre\":2,\"Price\":82.29,\"Pages\":3625,\"PublishedOn\":\"2014-11-09T00:00:00\"},{\"Id\":51,\"Name\":\"Common Netbush\",\"Genre\":2,\"Price\":77.83,\"Pages\":2179,\"PublishedOn\":\"2013-01-26T00:00:00\"},{\"Id\":52,\"Name\":\"Ethiopian Rattlebox\",\"Genre\":3,\"Price\":87.96,\"Pages\":1238,\"PublishedOn\":\"2014-09-03T00:00:00\"},{\"Id\":53,\"Name\":\"Pineapple\",\"Genre\":2,\"Price\":36.57,\"Pages\":117,\"PublishedOn\":\"2018-10-30T00:00:00\"},{\"Id\":54,\"Name\":\"Roble De Sierra\",\"Genre\":3,\"Price\":53.68,\"Pages\":588,\"PublishedOn\":\"2014-08-25T00:00:00\"},{\"Id\":55,\"Name\":\"Dicranodontium Moss\",\"Genre\":1,\"Price\":38.01,\"Pages\":2258,\"PublishedOn\":\"2014-10-07T00:00:00\"},{\"Id\":56,\"Name\":\"Jolon Clarkia\",\"Genre\":2,\"Price\":40.6,\"Pages\":4076,\"PublishedOn\":\"2014-06-13T00:00:00\"},{\"Id\":57,\"Name\":\"Candle Tree\",\"Genre\":1,\"Price\":9,\"Pages\":3917,\"PublishedOn\":\"2016-06-02T00:00:00\"},{\"Id\":58,\"Name\":\"Java Grass\",\"Genre\":1,\"Price\":62.49,\"Pages\":1123,\"PublishedOn\":\"2015-01-10T00:00:00\"},{\"Id\":59,\"Name\":\"Jasmine Nightshade\",\"Genre\":1,\"Price\":84.89,\"Pages\":900,\"PublishedOn\":\"2016-05-12T00:00:00\"},{\"Id\":60,\"Name\":\"Spanish Heath\",\"Genre\":2,\"Price\":1.13,\"Pages\":4450,\"PublishedOn\":\"2015-08-28T00:00:00\"},{\"Id\":61,\"Name\":\"Chesapeake Panicgrass\",\"Genre\":1,\"Price\":96.63,\"Pages\":2794,\"PublishedOn\":\"2017-06-14T00:00:00\"},{\"Id\":62,\"Name\":\"Little Elephantshead\",\"Genre\":3,\"Price\":41.81,\"Pages\":4786,\"PublishedOn\":\"2014-12-16T00:00:00\"},{\"Id\":63,\"Name\":\"Fringed Nutrush\",\"Genre\":1,\"Price\":96.94,\"Pages\":3491,\"PublishedOn\":\"2016-04-24T00:00:00\"},{\"Id\":64,\"Name\":\"Arrowleaf Clover\",\"Genre\":2,\"Price\":1.71,\"Pages\":2603,\"PublishedOn\":\"2016-05-27T00:00:00\"},{\"Id\":65,\"Name\":\"Spreading Beaksedge\",\"Genre\":1,\"Price\":2.43,\"Pages\":2804,\"PublishedOn\":\"2019-05-22T00:00:00\"},{\"Id\":66,\"Name\":\"Southern Pepperwort\",\"Genre\":3,\"Price\":63.48,\"Pages\":4230,\"PublishedOn\":\"2017-06-11T00:00:00\"},{\"Id\":67,\"Name\":\"Allen Fissidens Moss\",\"Genre\":2,\"Price\":78.44,\"Pages\":2825,\"PublishedOn\":\"2013-02-26T00:00:00\"},{\"Id\":68,\"Name\":\"Common Mullein\",\"Genre\":3,\"Price\":37.67,\"Pages\":4751,\"PublishedOn\":\"2018-07-03T00:00:00\"},{\"Id\":69,\"Name\":\"Palo Blanco\",\"Genre\":3,\"Price\":79.11,\"Pages\":3039,\"PublishedOn\":\"2014-06-25T00:00:00\"},{\"Id\":70,\"Name\":\"Narrowleaf Colicwood\",\"Genre\":2,\"Price\":9.09,\"Pages\":3134,\"PublishedOn\":\"2017-02-03T00:00:00\"}]}";

        var datasets = JsonConvert.DeserializeObject<Dictionary<string, IEnumerable<JObject>>>(datasetsJson);

        foreach (var dataset in datasets)
        {
            var entityType = GetType(dataset.Key);
            var entities = dataset.Value
                .Select(j => j.ToObject(entityType))
                .ToArray();

            context.AddRange(entities);
        }

        context.SaveChanges();
    }

    private static Type GetType(string modelName)
    {
        var modelType = CurrentAssembly
            .GetTypes()
            .FirstOrDefault(t => t.Name == modelName);

        Assert.IsNotNull(modelType, $"{modelName} model not found!");

        return modelType;
    }

    private static IServiceProvider ConfigureServices<TContext>(string databaseName)
        where TContext : DbContext
    {
        var services = ConfigureDbContext<TContext>(databaseName);

        var context = services.GetService<TContext>();

        try
        {
            context.Model.GetEntityTypes();
        }
        catch (InvalidOperationException ex) when (ex.Source == "Microsoft.EntityFrameworkCore.Proxies")
        {
            services = ConfigureDbContext<TContext>(databaseName, useLazyLoading: true);
        }

        return services;
    }

    private static IServiceProvider ConfigureDbContext<TContext>(string databaseName, bool useLazyLoading = false)
        where TContext : DbContext
    {
        var services = new ServiceCollection()
          .AddDbContext<TContext>(t => t
          .UseInMemoryDatabase(Guid.NewGuid().ToString())
          );

        var serviceProvider = services.BuildServiceProvider();
        return serviceProvider;
    }
}