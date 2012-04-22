iiInfinityEngine Alpha 4
2012/04/01

A C# object wrapper layer for the BG2 Infinity Engine files.


Partial todo list
/*
 * We can parallelize the bif reading, but need to check the mega-mod crowd who bif their overrides - do they amend existing file entries, or just append a new bif 
 * on the end of the .key and hope that last entry wins?
 * 
 * File formats
 *   Bcs - tokenize
 *   Dlg
 *   BAM |
 *   MOS | - converters
 *   TIS |
 *   Support for other file verions?
 *   
 * dialogf.tlk
 * 
 * File cleanup
 *
 * Load/Save from Xml files
 * Web-service file repository...?
 * Mod version checking web-service?
 * Nicer IDS / 2da wrapping
 */

History
Alpha 4 (Del) 2012/04/00
 - Started code documentation
 - Script compiler completed 
 - Script decompiler completed
 - BIFC files supported
 - LoadResources added as alternative to LoadAllResources

Alpha 3 (Kerr)  2012/03/25
 - Added sample app
 - Script compiler (lacking ActionOverride support)
 - Script decompiler (lacking ActionOverride support)

Alpha 2  (Olag) 2012/03/18
- Implicit BOIIC
- Backup changed files
- Uninstall support
- 90% complete script decompiler (needs serious refactoring though!)
- IDS wrapper methods
- Start porting MOS PS plugin code

Alpha 1 (Roj) 2012/02/29
- Initial release

iiInfinityEngine Alpha 4 by igi is licensed under a Creative Commons Attribution-NonCommercial-NoDerivs 3.0 Unported License.

igi
[EOF]