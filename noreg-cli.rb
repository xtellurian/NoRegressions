class NoregCli < Formula
    desc "Deploy machine learning with confidence."
    homepage "https://github.com/JasonTheDeveloper/NoRegressions"
    url "https://github.com/xtellurian/NoRegressions/releases/download/v0.0.3/noreg-osx-x64.tar.gz"
    sha256 "a7d095be2cf470951b8b696e6046942f2862656d3e66b7e6b9d7d8b7615daa6c"
  
    def install
        mv "./noreg-cli", "./noreg"
        bin.install Dir["./*"]
    end
    
    def post_install
        system "chmod -R 666 #{bin}/configsettings.json"
    end
  
    test do
        system "false"
    end
end

__END__
