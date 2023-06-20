var gulp = require("gulp"),
  rename = require("gulp-rename");
const sass = require("gulp-sass")(require("sass"));
//style paths
var sassFiles = "assets/scss/site.scss",
  cssDest = "wwwroot/css/";

gulp.task("default", function () {
  return gulp
    .src(sassFiles)
    .pipe(sass().on("error", sass.logError))
    .pipe(rename({}))
    .pipe(gulp.dest(cssDest));
});

gulp.task("watch", function () {
  gulp.watch("assets/scss/*.scss", gulp.series("default"));
});
