RunUO Extended, based on RunUO 2.0 FINAL

This is a third party extension to [Run UO](http://www.runuo.com/) and do not involve the original team. The logo at the top of this page is stolen from them too :)

You can checkout the svn to get an empty server running with all the modifications we made to the original RunUO 2.0 Final version. You can use the .sln with Visual Studio 2010, but you can't compile and run, you must use RunUO.exe for that.


---

_For specific changes and to help you modify them, here's a summary:_

**Farming System:**
  * [Eggs & legs from chicken](http://code.google.com/p/runuo-extended/source/detail?r=19)
**Utils:**
  * [Change Command Handler](http://code.google.com/p/runuo-extended/source/detail?r=5) because "." is better than "["
  * [Change Server Name](http://code.google.com/p/runuo-extended/source/detail?r=6) because "RunUO RT" is probably not what you want
  * [Change Starting Location](http://code.google.com/p/runuo-extended/source/detail?r=9) because Malas is the only cool map :)
  * [Autosave 15min instead of 5min](http://code.google.com/p/runuo-extended/source/detail?r=14) because 5min is SPAM :p
  * [SaveGump instead of TextMessage](http://code.google.com/p/runuo-extended/source/detail?r=15) because it looks pretty
  * [Translation Center](http://code.google.com/p/runuo-extended/source/detail?r=21) because french is cool to
**Commands:**
  * [Commands for GMs](http://code.google.com/p/runuo-extended/source/browse/RunUOExtended/trunk/RunUOExtended/Scripts/Commands/StaffCommands.cs?spec=svn27&r=27): HearAll Clone CloneMe Control GmMe Refresh SayThis (needs [AI\_None](http://code.google.com/p/runuo-extended/source/detail?r=10))
  * [.who for players + regions](http://code.google.com/p/runuo-extended/source/detail?r=13) because players like to know who is online too !
**Crafting:**
  * [Some ML Items](http://code.google.com/p/runuo-extended/source/detail?r=26): Some ML items for balcksmith that was not craftable but available in game.
**Items:**
  * [Horse Pole](http://code.google.com/p/runuo-extended/source/browse/RunUOExtended/trunk/RunUOExtended/Scripts/Items/Misc/HorsePole.cs?spec=svn11&r=11): To keep your horses yours between connections (needs [AI\_None](http://code.google.com/p/runuo-extended/source/detail?r=10))
**AI:**
  * [AI\_None](http://code.google.com/p/runuo-extended/source/detail?r=10), an easy way to have non-movable, non-agressive creatures (Dolls !)
  * [Animal fear](http://code.google.com/p/runuo-extended/source/detail?r=18) is making small animals running away when you're approaching
**Vendors:**
  * [Barber](http://code.google.com/p/runuo-extended/source/browse/RunUOExtended/trunk/RunUOExtended/Scripts/Mobiles/Vendors/NPC/Barber.cs?spec=svn7&r=7) will keep your hair and beard up to date !