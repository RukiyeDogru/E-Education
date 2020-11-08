/**
 * @license Copyright (c) 2003-2014, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function( config ) {
    config.language = 'tr';
    
    config.extraPlugins = 'html5video,widget,widgetselection,clipboard,lineutils';

    config.toolbarGroups = [
		{ name: 'document', groups: ['mode', 'document', 'doctools', 'tools'] },
		{ name: 'clipboard', groups: ['clipboard', 'undo'] },
		{ name: 'editing', groups: ['find', 'selection', 'spellchecker', 'editing'] },
		{ name: 'forms', groups: ['forms'] },
		'/',
		{ name: 'basicstyles', groups: ['basicstyles', 'cleanup'] },
		{ name: 'paragraph', groups: ['list', 'indent', 'blocks', 'align', 'bidi', 'paragraph'] },
		{ name: 'links', groups: ['links'] },
		{ name: 'insert', groups: ['insert'] },
		'/',
		{ name: 'styles', groups: ['styles'] },
		{ name: 'colors', groups: ['colors'] },
		{ name: 'others', groups: ['others'] },
		{ name: 'about', groups: ['about'] }
    ];

    config.removeButtons = 'Save,NewPage,Print,Preview,Templates,Scayt,SelectAll,Find,Replace,HiddenField,ImageButton,Button,Select,Textarea,TextField,Radio,Checkbox,Form,CreateDiv,BidiRtl,Language,BidiLtr,Flash,Smiley,About';

    config.filebrowserUploadUrl = '/genel/ckeditor-file-upload';
};