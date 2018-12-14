class NoregCli < Formula
    desc "Deploy machine learning with confidence."
    homepage "https://github.com/JasonTheDeveloper/NoRegressions"
    url "https://github.com/JasonTheDeveloper/NoRegressions/releases/download/v0.0.2-mac/noreg-osx-x64.tar.gz"
    sha256 "6f2fd018e44f65845918004ee0dcf45594a31b6a93411477245b4a78d263d089"
  
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
