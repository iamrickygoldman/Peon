module PeonHelper

let getDirectories (path:string) :System.IO.DirectoryInfo[] =
    let dir = new System.IO.DirectoryInfo(path)
    dir.GetDirectories()

let cleanFileName (name:string) :string =
    name.Replace(":","")

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