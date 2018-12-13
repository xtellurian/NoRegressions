class NoRegressions < Formula
    desc ""
    homepage ""
    url "https://github.com/JasonTheDeveloper/NoRegressions/releases/download/v0.0.2-mac/noreg-osx-x64.tar.gz"
    sha256 "bcaa929d2dd14137c41551983fa9a3e78e98262d2c8f937edaf89850fa472cac"
  
    def install
        mv publish/"cli", publish/"noreg"
        bin.install "./publish/noreg"
    end
  
    test do
        system "false"
    end
end