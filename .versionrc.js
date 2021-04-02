const tracker = {
  filename: "Directory.Build.props",
  updater: "csproj-version-updater.js",
};

module.exports = {
  packageFiles: [tracker],
  bumpFiles: [tracker],
};
