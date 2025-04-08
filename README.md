# 🐍 Slangespillet

En moderne versjon av det klassiske slangespillet bygget med Blazor WebAssembly.
Målet med prosjektet var å lære meg mest mulig C# og .NET.
Spillet inneholder også noen unike funksjoner som kaffeboost, En global poengtavle lagret i Supabase, og responsivt design for både PC og mobiltelefoner!

Mye av koden er basert fra en Youtube tutorial:
https://www.youtube.com/watch?v=uzAXxFBbVoE&t=4506s
Dette var en utrolig bra tutorial for å lære seg grunnleggende C# og .NET. Gi gjerne din støtte til OttoBotCode! 

Assets filene er laget av OttoBotCode, jeg har endret litt på utseende og fargene ellers er de ganske like hans design:
https://ottobotcode.com/snake/
Jeg har også lagt til en eget asset: Coffee.png, her kan du også lage dine egne assets med ulike funksjoner! prøv deg frem! 

## 🎮 Spill Online

Besøk [sett inn Vercel URL når den er publisert] for å spille direkte i nettleseren din!

## 🚀 Funksjoner

- 🎯 Klassisk slangespill 
- ☕ Kaffeboost for midlertidig fartøkning i 5 sekunder
- 🏆 Global poengtavle lagret i Supabase
- 📱 Responsivt design (fungerer på både PC og mobil)
- ⌨️ Tastatur- og berøringskontroller

## 🛠️ Forutsetninger

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- En moderne nettleser
- [Git](https://git-scm.com/downloads) (for å klone prosjektet)

## 📥 Installasjon

1. Klone prosjektet:
```bash
git clone https://github.com/yourusername/SnakeWasm.git
cd SnakeWasm
```

2. Opprett en `appsettings.Development.json` fil i SnakeWasm-mappen for å kunne koble på din egen database!:
```json
{
  "Supabase": {
    "Url": "din_supabase_url",
    "AnonKey": "din_supabase_anon_key"
  }
}
```

3. Gjenopprett avhengigheter:
```bash
dotnet restore
```

## 🎮 Kjøre Spillet

1. For å kjøre spillet lokalt:
```bash
dotnet run
```

2. Åpne nettleseren din og gå til:
```
https://localhost:5001
```

## 🎯 Hvordan Spille

### Tastaturkontroller (Desktop):
- Piltaster for å endre retning
- Hvilken som helst tast for å starte spillet

### Berøringskontroller (Mobil):
- Sveip i hvilken som helst retning for å bevege slangen
- Trykk på skjermen for å starte spillet

### Spillregler:
- 🍎 Spis epler for å vokse og få poeng
- ☕ Samle kaffe for midlertidig fartøkning i 5 sekunder
- 🚫 Unngå å treffe vegger og deg selv så klart
- 📊 Send inn poengsummen din til den globale poengtavlen ved å skrive inn navnet ditt!

## 🏗️ Bygge for Produksjon

For å bygge spillet for produksjon:

```bash
dotnet publish -c Release
```

De publiserte filene vil være i `bin/Release/net8.0/publish/wwwroot`.

## 🔧 Utvikling

For å kjøre spillet i utviklingsmodus med automatisk oppdatering:

```bash
dotnet watch run
```

## 🤝 Bidra

Bidrag er velkomne! Føl deg fri til å sende inn en Pull Request.

## 📝 Lisens

Dette prosjektet er lisensiert under MIT-lisensen - se LICENSE-filen for detaljer.

## 🙏 Anerkjennelser

- Bygget med [Blazor WebAssembly](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor)
- Backend drevet av [Supabase](https://supabase.com)
- Distribuert på [Vercel](https://vercel.com) 