module PeonHelper

let cleanFileName (name:string) :string =
    let stripChars text (chars:string) =
        Array.fold (fun (s:string) c-> s.Replace(c.ToString(), "")) text (chars.ToCharArray())
    stripChars name "<>:\"/\\|?*"

let formatName (format:string) (file:TagLib.File) :string =
    let tag = file.Tag
    let mutable n:string = tag.Track.ToString()
    let mutable N:string = n
    if (tag.Track = 0u) then
        n <- "0"
        N <- "00"
    elif (tag.Track < 10u) then
        N <- "0" + tag.Track.ToString()
    let mutable T:string = ""
    if not (tag.Title = null) then
        T <- tag.Title
    let t:string = T.ToLower()
    let mutable A:string = "";
    if not (tag.FirstAlbumArtist = null) then
        A <- tag.FirstAlbumArtist
    elif not (tag.FirstPerformer = null) then
        A <- tag.FirstPerformer
    elif not (tag.FirstComposer = null) then
        A <- tag.FirstComposer
    let a:string = A.ToLower()
    let mutable B:string = ""
    if not (tag.Album = null) then
        B <- tag.Album
    let b:string = B.ToLower()

    let mutable formatted = format
    formatted <- formatted.Replace("?N",N)
    formatted <- formatted.Replace("?n",n)
    formatted <- formatted.Replace("?T",T)
    formatted <- formatted.Replace("?t",t)
    formatted <- formatted.Replace("?A",A)
    formatted <- formatted.Replace("?a",a)
    formatted <- formatted.Replace("?B",B)
    formatted <- formatted.Replace("?b",b)
    printfn "%s" formatted
    (cleanFileName formatted)

let getAudioMimeTypes :string[] =
    [|
        ".3gp"
        ".aa"
        ".aac"
        ".aax"
        ".act"
        ".aiff"
        ".amr"
        ".ape"
        ".au"
        ".awb"
        ".dct"
        ".dss"
        ".dvf"
        ".flac"
        ".gsm"
        ".iklax"
        ".ivs"
        ".m4a"
        ".m4b"
        ".m4p"
        ".mmf"
        ".mp3"
        ".mpc"
        ".msv"
        ".ogg"
        ".oga"
        ".mogg"
        ".opus"
        ".ra"
        ".rm"
        ".raw"
        ".sln"
        ".tta"
        ".vox"
        ".wav"
        ".wma"
        ".wv"
        ".webm"
    |]