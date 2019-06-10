/// <binding BeforeBuild='scss' ProjectOpened='move-fa' />
var gulp = require('gulp');
var sass = require('gulp-sass');

gulp.task('scss', function () {
    return gulp.src('scss/*')
        .pipe(sass({
            includePaths: ['node_modules']
        }))
        .pipe(gulp.dest('wwwroot/css'))
});

gulp.task('move-fa', function () {
    return gulp.src(['node_modules/@fortawesome/fontawesome-free/css/all.min.css')
        .pipe(gulp.dest('wwwroot/css'))
})

gulp.task('move-webfonts', function () {
    return gulp.src('node_modules/@fortawesome/fontawesome-free/webfonts/*')
        .pipe(gulp.dest('wwwroot/webfonts'))
})