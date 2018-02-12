/// <binding Clean='clean, scripts' />
/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp');
var uglify = require('gulp-uglify');
var gulpCopy = require('gulp-copy');
var concat = require('gulp-concat');
var rimraf = require("rimraf");
var merge = require('merge-stream');

//gulp.task("minify", function () {

//    var streams = [
//        gulp.src(["wwwroot/js/*.js", '!wwwroot/js/tour*.js', '!wwwroot/js/contact.js'])
//            .pipe(uglify())
//            .pipe(concat("wilderblog.min.js"))
//            .pipe(gulp.dest("wwwroot/lib/site")),
//        gulp.src(["wwwroot/js/contact.js"])
//            .pipe(uglify())
//            .pipe(concat("contact.min.js"))
//            .pipe(gulp.dest("wwwroot/lib/site"))
//    ];

//    return merge(streams);
//});

gulp.task("clean", function (cb) {
    //return rimraf("wwwroot/vendor/", cb);
});

gulp.task("scripts", function () {

    //var sourceFiles = ['node_modules/jquery/dist/*.js',
    //    'node_modules/bootstrap/dist/js/*.js',
    //    'node_modules/bootstrap/dist/css/*.css',
    //    'node_modules/popper.js/dist/umd/*.js'];
    //var destination = 'wwwroot/vendor';

    //return gulp
    //    .src(sourceFiles)
    //    .pipe(gulpCopy(destination, { prefix: 1}));
});

gulp.task("default", ['clean', 'scripts']);
